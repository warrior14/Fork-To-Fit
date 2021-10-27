using Microsoft.Data.SqlClient;
using ForkToFit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ForkToFit.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {

        private readonly IConfiguration _config;

        public UserProfileRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public UserProfile GetById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                    SELECT Id, Name, Email, FirebaseUserId, DateCreated, BmrInfo
                                    FROM UserProfile
                                    WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@id", id);

                    UserProfile userProfile = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        userProfile = new UserProfile
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            FirebaseUserId = reader.GetString(reader.GetOrdinal("FirebaseUserId")),
                            DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                            BmrInfo = reader.GetInt32(reader.GetOrdinal("BmrInfo"))
                        };
                    }
                    reader.Close();

                    return userProfile;
                }
            }
        }

        // method below gets a userProfile based on a user's profileId as a string (to be used when assigning the value
        // of the userProfileId to the current user in the controller method such as setting the integer value of the currently
        // logged in user to userProfileId when creating a meal plan for example, etc):
        public UserProfile GetByUserProfileId(string userProfileId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                    SELECT Id, Name, Email, FirebaseUserId, DateCreated, BmrInfo
                                    FROM UserProfile
                                    WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", userProfileId);

                    UserProfile userProfile = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        userProfile = new UserProfile
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            FirebaseUserId = reader.GetString(reader.GetOrdinal("FirebaseUserId")),
                            DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                            BmrInfo = reader.GetInt32(reader.GetOrdinal("BmrInfo"))
                        };
                    }
                    reader.Close();

                    return userProfile;
                }
            }
        }

        // method below gets a userProfile based on the firebaseUserId
        // which is a string (to be used for authentication when logging in by the AccountController):
        public UserProfile GetByFirebaseUserId(string firebaseUserId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                    SELECT Id, Name, Email, FirebaseUserId, DateCreated, BmrInfo
                                    FROM UserProfile
                                    WHERE FirebaseUserId = @FirebaseUserId";

                    cmd.Parameters.AddWithValue("@FirebaseUserId", firebaseUserId);

                    UserProfile userProfile = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        userProfile = new UserProfile
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            FirebaseUserId = reader.GetString(reader.GetOrdinal("FirebaseUserId")),
                            DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                            BmrInfo = reader.GetInt32(reader.GetOrdinal("BmrInfo"))
                        };
                    }
                    reader.Close();

                    return userProfile;
                }
            }
        }

        public void Add(UserProfile userProfile)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        INSERT INTO
                                        UserProfile (Email, FirebaseUserId, Name, DateCreated, BmrInfo) 
                                        OUTPUT INSERTED.ID
                                        VALUES(@email, @firebaseUserId, @name, @dateCreated, @bmrInfo)";

                    cmd.Parameters.AddWithValue("@email", userProfile.Email);
                    cmd.Parameters.AddWithValue("@firebaseUserId", userProfile.FirebaseUserId);
                    cmd.Parameters.AddWithValue("@name", userProfile.Name);
                    cmd.Parameters.AddWithValue("@dateCreated", userProfile.DateCreated);
                    cmd.Parameters.AddWithValue("@bmrInfo", userProfile.BmrInfo);

                    userProfile.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}
