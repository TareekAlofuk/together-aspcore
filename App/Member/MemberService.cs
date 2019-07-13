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

        public async Task<Member> CreateNewMember(Member member)
        {
            return await _memberRepository.Create(member);
        }

        public async Task<List<Member>> GetAllMembers()
        {
            return await _memberRepository.GetAll();
        }

        public async Task<Member> EditExistingMember(Member member)
        {
            return await _memberRepository.Edit(member);
        }

        public async Task<MemberCredentials> SaveCredentials(MemberCredentials memberCredentials)
        {
            memberCredentials.Password = BcryptHash.Bcrypt(memberCredentials.Password);
            return await _memberRepository.SaveCredentials(memberCredentials);
        }

//
//        public async Task<MemberCredentials> EditCredentialAsync(MemberCredentials memberCredentials)
//        {
//            memberCredentials.Password = BcryptHash.Bcrypt(memberCredentials.Password);
//            return await _memberRepository.EditCredential(memberCredentials);
//        }

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
            var fileName = Guid.NewGuid().ToString();
            var filePath = Path.Combine(rootPath, fileName);
            await file.CopyToAsync(new FileStream(filePath, FileMode.Create));
            if (!File.Exists(filePath)) throw new Exception("Cannot Save The File");
            return fileName;
        }

        public async Task<Member> GetMemberInfo(int id)
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


        public async Task<List<Member>> GetRecentlyAdded(int limit)
        {
            return await _memberRepository.GetRecentlyAdded(limit);
        }

        public async Task<List<Member>> FindMembers(string query)
        {
            var isNumber = query.All(char.IsNumber);
            if (isNumber)
            {
                return await _memberRepository.FindMembersById(int.Parse(query));
            }

            return await _memberRepository.FindMembersByName(query);
        }
    }
}