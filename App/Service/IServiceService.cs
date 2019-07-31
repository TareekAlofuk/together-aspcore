using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using together_aspcore.App.Member;
using together_aspcore.App.Service.Dto;
using together_aspcore.App.Service.Models;

namespace together_aspcore.App.Service
{
    public interface IServiceService
    {
        Task<ServiceUsage> RegisterService(ServiceUsage serviceUsage);
        Task UnregisterService(int serviceUsageId);

        Task<List<ServiceDetail>> GetServiceDetails(MembershipType membershipType);
        
        Task<List<MemberServiceQureyModel>> GetServiceUsageForMember(int memberId, DateTime? from = null,
            DateTime? to = null);
    }
}