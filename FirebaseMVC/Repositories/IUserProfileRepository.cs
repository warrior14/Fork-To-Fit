using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForkToFit.Models;

namespace ForkToFit.Repositories
{
    public interface IUserProfileRepository
    {
        void Add(UserProfile userProfile);
        // method below gets a userProfile based on the firebaseUserId
        // which is a string (to be used for authentication when logging in by the AccountController):
        UserProfile GetByFirebaseUserId(string firebaseUserId);
        // method below gets a userProfile based on a user's profileId as a string (to be used when assigning the value
        // of the userProfileId to the current user in the controller method such as setting the integer value of the currently
        // logged in user to userProfileId when creating a meal plan for example, etc):
        UserProfile GetByUserProfileId(string userProfileId);
        UserProfile GetById(int id);
    }
}