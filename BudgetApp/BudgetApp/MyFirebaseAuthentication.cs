using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp
{
    public interface MyFirebaseAuthentication
    {
        Task<string> LoginWithEmailAndPassword(string email, string password);
        Task<string> SignUpWithEmailAndPassword(string email, string password);

        bool SignOut();
        bool IsSignIn();
        string GetUid();
        string GetUname();
    }
}
