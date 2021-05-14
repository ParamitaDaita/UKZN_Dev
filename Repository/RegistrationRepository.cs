using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WroseModel.Models;
using WroseModel.View_Model;

namespace WroseModel.Repository
{
    public class RegistrationRepository : IRegistration
    {
        private WROSEDBEntities DBcontext;
        private Models.User_Registration user_Registration;
        public RegistrationRepository(WROSEDBEntities objregistrationcontext)
        {
            this.DBcontext = objregistrationcontext;
        }
        public int NewRegistration(RegistrationVM objRegistration)
        {
            int isSaved = 0;
            try
            {
                user_Registration = new User_Registration();
                user_Registration.First_Name = objRegistration.First_Name;
                user_Registration.Last_Name = objRegistration.Last_Name;
                user_Registration.User_Email = objRegistration.User_Email;
                user_Registration.User_Mobile = objRegistration.User_Mobile;
                user_Registration.User_Password = objRegistration.User_Password;
                user_Registration.Active=objRegistration.Active;
                user_Registration.User_Role_ID=objRegistration.User_Role_ID;
                user_Registration.Created_Date=objRegistration.Created_Date;
                DBcontext.User_Registration.Add(user_Registration);
                DBcontext.SaveChanges();
                return isSaved = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetUserByEmail(string email)
        {
            string useremail = string.Empty;
            try {
                var user = DBcontext.User_Registration
                   .Where(b => b.User_Email == email)
                   .FirstOrDefault();
                if (user != null)
                {
                    useremail = user.User_Email.ToString();
                }
                return useremail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DBcontext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}