using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using together_aspcore.Shared;

namespace together_aspcore.App.Member
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        private readonly IHostingEnvironment _hostingEnvironment;

        public MemberService(IMemberRepository memberRepository, IHostingEnvironment hostingEnvironment)
        {
            _memberRepository = memberRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<Models.Member> CreateNewMember(Models.Member member)
        {
            return await _memberRepository.Create(member);
        }

        public async Task<List<Models.Member>> GetAllMembers()
        {
            return await _memberRepository.GetAll();
        }

        public async Task<Models.Member> EditExistingMember(Models.Member member)
        {
            var existingMember = await _memberRepository.GetMemberInfo(member.Id);

            existingMember.Name = member.Name;
            existingMember.Title = member.Title;
            existingMember.Phone = member.Phone;
            existingMember.SecondaryPhone = member.SecondaryPhone;
            existingMember.Email = member.Email;
            existingMember.Type = member.Type;
            existingMember.ExpirationDate = member.ExpirationDate;
            existingMember.PassportNo = member.PassportNo;
            existingMember.PassportExpirationDate = member.PassportExpirationDate;
            existingMember.BirthDate = member.BirthDate;
            existingMember.Address = member.Address;
            existingMember.JobTitle = member.JobTitle;

            return await _memberRepository.Edit(existingMember);
        }

        public async Task<MemberCredentials> SaveCredentials(MemberCredentials memberCredentials)
        {
            memberCredentials.Password = BcryptHash.Bcrypt(memberCredentials.Password);
            return await _memberRepository.SaveCredentials(memberCredentials);
        }


        public async Task<MemberFile> StoreMemberAttachment(int memberId, string displayFileName, IFormFile formFile)
        {
            if (formFile == null) return null;

            var fileName = await SaveFileIntoLocalStorage(formFile);
            displayFileName = displayFileName ?? fileName;
            var memberFile = new MemberFile
                {FileName = fileName, DisplayFileName = displayFileName, MemberId = memberId};
            await _memberRepository.CreateMemberAttachment(memberFile);
            return memberFile;
        }

        private async Task<string> SaveFileIntoLocalStorage(IFormFile file)
        {
            //TODO : fix hard codded storage folder name
            var rootPath = Path.Combine(_hostingEnvironment.ContentRootPath, "Files");
            var extension = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid() + extension;
            var filePath = Path.Combine(rootPath, fileName);
            await file.CopyToAsync(new FileStream(filePath, FileMode.Create));
            if (!File.Exists(filePath)) throw new Exception("Cannot Save The File");
            return fileName;
        }

        public async Task<Models.Member> GetMemberInfo(int id)
        {
            return await _memberRepository.GetMemberInfo(id);
        }

        public async Task<bool> ChangeDisableStatus(int id, bool disable)
        {
            return await _memberRepository.ChangeDisableStatus(id, disable);
        }

        public async Task<bool> ChangeArchiveStatus(int id, bool archive)
        {
            return await _memberRepository.ChangeArchiveStatus(id, archive);
        }


        public async Task<List<Models.Member>> GetRecentlyAdded(int limit)
        {
            return await _memberRepository.GetRecentlyAdded(limit);
        }

        public async Task<List<Models.Member>> FindMembers(string query)
        {
            var isNumber = query.All(char.IsNumber);
            if (isNumber)
            {
                return await _memberRepository.FindMembersById(int.Parse(query));
            }

            return await _memberRepository.FindMembersByName(query);
        }

        public async Task<List<MemberFile>> GetMemberAttachments(int memberId)
        {
            return await _memberRepository.GetMemberAttachments(memberId);
        }

        public async Task<bool> DeleteAttachedFile(int fileId)
        {
            var memberFile = await _memberRepository.GetMemberFile(fileId);
            var rootPath = Path.Combine(_hostingEnvironment.ContentRootPath, "Files");
            var filePath = Path.Combine(rootPath, memberFile.FileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            return await _memberRepository.DeleteAttachedMemberFile(fileId);
        }

        public async Task<Models.Member> UpgradeMembership(int memberId, MembershipType newMembershipType,
            DateTime until)
        {
            return await _memberRepository.UpgradeMembership(memberId, until, newMembershipType);
        }

        public async Task<List<Models.Member>> GetMembersWithExpiredMembership()
        {
            return await _memberRepository.GetMembersWithExpiredMembership();
        }

        public async Task<List<Models.Member>> GetMembersWithNearlyExpiredMembership()
        {
            return await _memberRepository.GetMembersWithNearlyExpiredMembership();
        }

        public async Task<List<Models.Member>> GetMembersWithPassportWillExpire()
        {
            return await _memberRepository.GetMembersWithPassportWillExpire();
        }

        public async Task<List<Models.Member>> GetMembersWithBirthDate()
        {
            return await _memberRepository.GetMembersWithBirthDate();
        }

        public async Task<List<Models.Member>> GetMembersWithDisabledMembership()
        {
            return await _memberRepository.GetMembersWithDisabledMembership();
        }

        public async Task<List<Models.Member>> GetArchivedMembers()
        {
            return await _memberRepository.GetArchivedMembers();
        }

        public async Task<string> UploadPassport(int memberId, IFormFile passport)
        {
            var filename = await SaveFileIntoLocalStorage(passport);
            var member = await _memberRepository.GetMemberInfo(memberId);
            member.PassportImage = filename;
            await _memberRepository.Edit(member);
            return filename;
        }

        public async Task<string> UploadIdentityImage(int memberId, IFormFile identityImage)
        {
            var filename = await SaveFileIntoLocalStorage(identityImage);
            var member = await _memberRepository.GetMemberInfo(memberId);
            member.FaceImage = filename;
            await _memberRepository.Edit(member);
            return filename;
        }
    }
}