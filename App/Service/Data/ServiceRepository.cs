using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using together_aspcore.App.Member;
using together_aspcore.App.Service.Dto;
using together_aspcore.App.Service.Models;
using together_aspcore.Shared;

namespace together_aspcore.App.Service
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly TogetherDbContext _context;

        public ServiceRepository(TogetherDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceRule> GetServiceRule(MembershipType membershipType, Models.Service service)
        {
            return await _context.ServicesRules
                .Where(x => x.Service.Id == service.Id && x.MembershipType == membershipType)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> DoesHaveUnexpiredService(Member.Models.Member member, Models.Service service)
        {
            var today = DateTime.Now;
            var count = await _context.ServicesStore.Where(x =>
                    x.Service.Id == service.Id && x.Member.Id == member.Id && x.ExpirationDate < today)
                .CountAsync();
            return count > 0;
        }

        public async Task<ServiceUsage> SaveServiceUsage(ServiceUsage serviceUsage)
        {
            serviceUsage.Time = DateTime.Now;
            var entity = await _context.ServicesUsages.AddAsync(serviceUsage);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<ServiceStore> DecreaseServiceStore(Member.Models.Member member, Models.Service service)
        {
            var today = DateTime.Now;

            var store = await _context.ServicesStore.Where(
                    x => x.Service.Id == service.Id && x.Member.Id == member.Id && x.ExpirationDate < today)
                .ToListAsync();

            var rules = await GetServiceRule(member.Type, service);
            if (!store.Any() && rules.LimitType == ServiceLimitType.LIMITED)
                throw new ServiceException(ServiceErrorCode.NOT_ENOUGH_IN_STORE);

            if (!store.Any())
            {
                return null;
            }
            
            _context.ServicesStore.Remove(store[0]);
            await _context.SaveChangesAsync();
            return store[0];
        }

        public async Task<ServiceUsage> GetServiceUsage(int serviceUsageId)
        {
            return await _context.ServicesUsages.FindAsync(serviceUsageId);
        }

        public async Task IncreaseServiceStore(Member.Models.Member member, Models.Service service,
            DateTime expirationDate)
        {
            var serviceStore = new ServiceStore
                {ExpirationDate = expirationDate.Date, Member = member, Service = service};
            await _context.AddAsync(serviceStore);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteServiceUsage(int serviceUsageId)
        {
            var serviceUsage = await _context.ServicesUsages.FindAsync(serviceUsageId);
            _context.ServicesUsages.Remove(serviceUsage);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ServiceDetail>> GetServiceDetail(MembershipType membershipType)
        {
            var membershipTypeParam = new NpgsqlParameter("membershipType", (int) membershipType);
            var sql = "SELECT \"Services\".\"Id\" , \"Title\" , \"LimitType\" , \"Discount\" , \"DiscountOptions\"" +
                      "\nFROM \"Services\" JOIN \"ServicesRules\"" +
                      "\nON \"Services\".\"Id\" = \"ServicesRules\".\"ServiceId\"" +
                      "\nWHERE \"ServicesRules\".\"MembershipType\" = @membershipType";
            return await _context.ServiceDetails
                .FromSql(sql, membershipTypeParam)
                .ToListAsync();
        }
    }
}