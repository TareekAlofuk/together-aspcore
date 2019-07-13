using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using together_aspcore.Shared;

namespace together_aspcore.App.Member
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public IHostingEnvironment _hostingEnvironment { get; }

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

        public async Task<Credential> SetCredentialAsync(Credential credential)
        {
            credential.Password = BcryptHash.Bcrypt(credential.Password);
            return await _memberRepository.SetCredential(credential);
        }

        public async Task<Credential> EditCredentialAsync(Credential credential)
        {
            credential.Password = BcryptHash.Bcrypt(credential.Password);
            return await _memberRepository.EditCredential(credential);
        }

        public async Task<File> SaveFile(int id ,IFormFile formFile)
        {
            if (formFile != null)
            {

                string rootPath = Path.Combine(_hostingEnvironment.ContentRootPath, "Files");
                string fileName = Guid.NewGuid().ToString() + ".jpg";
                string filePath = Path.Combine(rootPath, fileName);
                await formFile.CopyToAsync(new FileStream(filePath, FileMode.Create));

                
                if (System.IO.File.Exists(filePath))
                {
                    File file = new File
                    {
                        Path = fileName,
                        
                    };

                    await _memberRepository.SaveFile( id,file);
                    return file;
                }

            }
            return null;

        }
    }
}