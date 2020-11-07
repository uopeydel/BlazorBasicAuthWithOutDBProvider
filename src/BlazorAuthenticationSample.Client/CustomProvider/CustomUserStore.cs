using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using System.Threading;


namespace BlazorAuthenticationSample.Client.CustomProvider
{
    public class CustomUserStore : IUserStore<ApplicationUser>,
         IUserPasswordStore<ApplicationUser>
    {
        //private readonly DapperUsersTable _usersTable;

        public CustomUserStore(
            //DapperUsersTable usersTable
            )
        {
            //_usersTable = usersTable;
        }

        #region createuser
        public async Task<IdentityResult> CreateAsync(ApplicationUser user,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return IdentityResult.Success;
            //return await _usersTable.CreateAsync(user);
        }
        #endregion

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));


            return IdentityResult.Success;
            //return await _usersTable.DeleteAsync(user);

        }

        public void Dispose()
        {
        }

        /// <summary>
        /// use1
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ApplicationUser> FindByIdAsync(string userId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (userId == null) throw new ArgumentNullException(nameof(userId));
            //Guid idGuid;
            //if (!Guid.TryParse(userId, out idGuid))
            //{
            //    throw new ArgumentException("Not a valid Guid id", nameof(userId));
            //}

            return new ApplicationUser() { UserId = userId };
            //return await _usersTable.FindByIdAsync(idGuid);

        }

        public async Task<ApplicationUser> FindByNameAsync(string userName,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (userName == null) throw new ArgumentNullException(nameof(userName));
            return new ApplicationUser() {UserId = userName };
            //return await _usersTable.FindByNameAsync(userName);
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.PassWord);
            //return Task.FromResult(user.PasswordHash);
        }


        /// <summary>
        /// use1 signin1
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.UserId);
            //return Task.FromResult(user.Id.ToString());
        }

        /// <summary>
        /// use1 signin2
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.UserId);
            //return Task.FromResult(user.UserName);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (normalizedName == null) throw new ArgumentNullException(nameof(normalizedName));

            return Task.FromResult(user.UserId);
            //user.NormalizedUserName = normalizedName;
            //return Task.FromResult<object>(null);
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (passwordHash == null) throw new ArgumentNullException(nameof(passwordHash));

            return Task.FromResult(user.PassWord);
            //user.PasswordHash = passwordHash;
            //return Task.FromResult<object>(null);

        }

        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
