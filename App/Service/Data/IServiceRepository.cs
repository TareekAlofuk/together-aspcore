using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using together_aspcore.App.Member;
using together_aspcore.App.Service.Dto;
using together_aspcore.App.Service.Models;

namespace together_aspcore.App.Service
{
    public interface IServiceRepository
    {
        Task<ServiceRule> GetServiceRule(MembershipType membershipType, Models.Service service);
        Task<bool> DoesHaveUnexpiredService(Member.Models.Member member, Models.Service service);
        Task<ServiceUsage> SaveServiceUsage(ServiceUsage serviceUsage);
        Task<ServiceStore> DecreaseServiceStore(Member.Models.Member member, Models.Service service);
        Task<ServiceUsage> GetServiceUsage(int serviceUsageId);
        Task IncreaseServiceStore(Member.Models.Member member, Models.Service service , DateTime expirationDate);
        Task DeleteServiceUsage(int serviceUsageId);
        Task<List<ServiceDetail>> GetServiceDetail(MembershipType membershipType);
        
        Task<List<MemberServiceQureyModel>> GetServicesUsageForMember(int memberId, DateTime? from, DateTime? to);

    }
}