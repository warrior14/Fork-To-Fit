using System.Threading.Tasks;
using ForkToFit.Auth.Models;

namespace ForkToFit.Auth
{
    public interface IFirebaseAuthService
    {
        Task<FirebaseUser> Login(Credentials credentials);
        Task<FirebaseUser> Register(Registration registration);
    }
}