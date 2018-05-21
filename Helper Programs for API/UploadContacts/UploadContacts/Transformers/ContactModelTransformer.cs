using System.Collections.Generic;
using Trackmatic.Rest.Core.Model;
using UploadContacts.Csv;

namespace UploadContacts.Transformers
{
    class ContactModelTransformer
    {
        private readonly List<ContactLines> _contacts;

        public ContactModelTransformer(List<ContactLines> contacts)
        {
            _contacts = contacts;
        }

        public List<ContactInfo> Transform()
        {
            var contactsToUpload = new List<ContactInfo>();

            foreach (var contact in _contacts)
            {
                contactsToUpload.Add(new ContactInfo
                {
                    //Title = 
                    FirstName = contact.GetFirstName(),
                    LastName = contact.GetLastName(),
                    IdentityNumber = contact.GetIdentityNumber(),
                    Email = contact.GetEmail(),
                    DepartmentPosition = contact.GetDepartmentPosition(),
                    Mobile = contact.GetMobile(),
                    HomeTel = contact.GetHomeTel(),
                    WorkTel = contact.GetWorkTel()
                });
            }
            return contactsToUpload;
        }
    }
}
