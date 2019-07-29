using System.Collections.Generic;
using System.Threading.Tasks;
using together_aspcore.App.Member;
using together_aspcore.App.Service.Dto;
using together_aspcore.App.Service.Models;

namespace together_aspcore.App.Service
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMemberRepository _memberRepository;

        public ServiceService(IServiceRepository serviceRepository, IMemberRepository memberRepository)
        {
            _serviceRepository = serviceRepository;
            _memberRepository = memberRepository;
        }

        public async Task<ServiceUsage> RegisterService(ServiceUsage serviceUsage)
        {
            var member = await _memberRepository.GetMemberInfo(serviceUsage.Member.Id);
            var service = serviceUsage.Service;

            if (!await IsAllowToUse(member, service) || !await HaveEnoughInStore(member, service))
                throw new ServiceException(ServiceErrorCode.NOT_ENOUGH_IN_STORE);

            await _serviceRepository.DecreaseServiceStore(member, service);

            return await _serviceRepository.SaveServiceUsage(serviceUsage);
        }

        public async Task UnregisterService(int serviceUsageId)
        {
            var serviceUsage = await _serviceRepository.GetServiceUsage(serviceUsageId);
            var member = serviceUsage.Member;
            var service = serviceUsage.Service;
            if (serviceUsage == null)
                throw new ServiceException(ServiceErrorCode.SERVICE_NOT_FOUND);
            if (serviceUsage.ExpirationDate == null)
            {
                throw new ServiceException(ServiceErrorCode.CANNOT_UNREGISTER_UNLIMITED_SERVICE);
            }

            await _serviceRepository.DeleteServiceUsage(serviceUsage.Id);
            await _serviceRepository.IncreaseServiceStore(member, service, serviceUsage.ExpirationDate.Value);
        }

        public async Task<List<ServiceDetail>> GetServiceDetails(MembershipType membershipType)
        {
            return await _serviceRepository.GetServiceDetail(membershipType);
        }

        private async Task<bool> HaveEnoughInStore(Member.Models.Member member, Models.Service service)
        {
            return await _serviceRepository.DoesHaveUnexpiredService(member, service);
        }


        private async Task<bool> IsAllowToUse(Member.Models.Member member, Models.Service service)
        {
            var rule = await _serviceRepository.GetServiceRule(member.Type, service);
            return rule != null;
        }
    }
}