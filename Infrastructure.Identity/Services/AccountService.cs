using Core.Application.DTOS.Account;
using Core.Application.DTOS.Email;
using Core.Application.Enum;
using Core.Application.Inferfaces.Service;
using Core.Domain.Settings;
using Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly JWTSettings _jWTSettings;

        public AccountService(UserManager<ApplicationUser> userManager,IOptions<JWTSettings> options, SignInManager<ApplicationUser> signInManager, IEmailService emailService)
        {
            _signInManager = signInManager;
            _emailService = emailService;
            _userManager = userManager;
            _jWTSettings = options.Value;
        }
        public async Task<AuthenticationResponse> Authentication(AuthenticationRequest request)
        {
            var response = new AuthenticationResponse();
            var User = await _userManager.FindByNameAsync(request.UserName);
            if (User == null)
            {
                response.HasError = true;
                response.Error = $"No Account Register with {request.UserName}";
                return response;
            }
            var result = await _signInManager.PasswordSignInAsync(User.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid Password";
                return response;
            }
            if (!User.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Account not confirm for {request.UserName}";
                return response;
            }
 
            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(User);

            response.Id = User.Id;
            response.Name = User.Name;
            response.LastName = User.LastName;
            response.Email = User.Email;
            response.IsVerified = User.EmailConfirmed;
            response.ImageProfile = User.ImageProfile;
            var roles = await _userManager.GetRolesAsync(User).ConfigureAwait(false);
            response.Roles = roles.ToList();
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var refreshToken = GenerateRefreshToken();
            response.RefreshToken = refreshToken.Token;

            return response;
        }

        public async Task<RegisterResponse> Register(RegisterRequest request, string origin)
        {
            var response = new RegisterResponse();
            var UserNameExist = await _userManager.FindByNameAsync(request.UserName);
            if (UserNameExist != null)
            {
                response.HasError = true;
                response.Error = $"Username {request.UserName} is already taken";
                return response;
            }
            var EmailExist = await _userManager.FindByEmailAsync(request.Email);
            if (EmailExist != null)
            {
                response.HasError = true;
                response.Error = $"Email {request.Email} is already registered";
                return response;
            }
            var user = new ApplicationUser
            {
                Email = request.Email,
                Name = request.Name,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.Phone,
                ImageProfile = request.ImageProfile

            };
            if (request.UserType == Roles.Cliente.ToString())
            {

                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                {
                    response.HasError = true;
                    response.Error = "A error occurred trying to register the user.";
                    return response;

                }
                var regiteredUser = await _userManager.FindByEmailAsync(user.Email);
                response.Id = regiteredUser.Id;
                await _userManager.AddToRoleAsync(user, Roles.Cliente.ToString());
                var verificationUrl = await SendVerificationEMailUrl(user, origin);
                await _emailService.Send(new EmailRequest()
                {
                    To = user.Email,
                    Body = $"Please confirm your account visiting this URL {verificationUrl}",
                    Subject = "Confirm registration"
                });
            } 
            else if (request.UserType == Roles.Agente.ToString())
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                {
                    response.HasError = true;
                    response.Error = "A error occurred trying to register the user.";
                    return response;

                }
                var regiteredUser = await _userManager.FindByEmailAsync(user.Email);
                response.Id = regiteredUser.Id;
                await _userManager.AddToRoleAsync(user, Roles.Agente.ToString());
            }
            else if (request.UserType == Roles.Administrador.ToString())
            {
                user.EmailConfirmed = true;
                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                {
                    response.HasError = true;
                    response.Error = "A error occurred trying to register the user.";
                    return response;

                }
                var regiteredUser = await _userManager.FindByEmailAsync(user.Email);
                response.Id = regiteredUser.Id;
                await _userManager.AddToRoleAsync(user, Roles.Administrador.ToString());
            }
            else if (request.UserType == Roles.Desarrollador.ToString())
            {
                user.EmailConfirmed = true;
                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                {
                    response.HasError = true;
                    response.Error = "A error occurred trying to register the user.";
                    return response;

                }
                var regiteredUser = await _userManager.FindByEmailAsync(user.Email);
                response.Id = regiteredUser.Id;
                await _userManager.AddToRoleAsync(user, Roles.Desarrollador.ToString());
            }

            return response;
        }
        public async Task<GenericResponse> UpdateUser(string userId, RegisterRequest request)
        {
            var response = new RegisterResponse();
            var user = await _userManager.FindByIdAsync(userId);
            user.ImageProfile = request.ImageProfile;
            if(request.Name != null)
            {
                user.Name = request.Name;
            }
            if (request.LastName != null)
            {
                user.LastName = request.LastName;
            }
            if (request.Phone != null)
            {
                user.PhoneNumber = request.Phone;
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = "A error occurred trying to register the user.";
                return response;

            }
            return response;
        }

        private async Task<string> SendVerificationEMailUrl(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "Account/EmailConfirm";
            var url = new Uri(string.Concat($"{origin}/", route));
            var verificationUrl = QueryHelpers.AddQueryString(url.ToString(), "userId", user.Id);
            verificationUrl = QueryHelpers.AddQueryString(verificationUrl, "token", code);

            return verificationUrl;
        }

        public async Task<string> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return $"Not account registered with this user";
            }
            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return $"An error occurred while confirming {user.Email}";
            }
            return $"Account confirmed for {user.Email}. You can now use the App";

        }

        private async Task<string> SendForgotPasswordUrl(ApplicationUser user, string origin)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "Account/ResetPassword";
            var url = new Uri(string.Concat($"{origin}/", route));
            var verificationUrl = QueryHelpers.AddQueryString(url.ToString(), "token", code);



            return verificationUrl;
        }

        public async Task<GenericResponse> ForgotPassword(ForgotPasswordRequest request, string origin)
        {
            var response = new GenericResponse();
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No account registered with {request.Email}";
                return response;
            }
            var verificationUrl = await SendForgotPasswordUrl(user, origin);
            await _emailService.Send(new EmailRequest()
            {
                To = user.Email,
                Body = $"Please reset your account visiting this URL {verificationUrl}",
                Subject = "Reset Password"
            });

            return response;
        }

        public async Task<GenericResponse> ResetPassword(ResetPasswordRequest request)
        {
            var response = new GenericResponse();
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No account registered with {request.Email}";
                return response;
            }
            request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error occurred while reset password";
                return response;
            }

            return response;
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        //public List<UserViewModel> GetAllUser()
        //{
        //    var users = _userManager.Users.ToList();
        //    List<UserViewModel> usersList = users.Select(user => new UserViewModel
        //    {
        //        Id = user.Id,
        //        Name = user.Name,
        //        LastName = user.LastName,
        //        UserName = user.UserName,
        //        Identification = user.Identification,
        //        SavingsAccount = user.SavingAccount

        //    }).ToList();

        //    return usersList;
        //}

        public List<AuthenticationResponse> GetAllAgents()
        {
            var users = _userManager.GetUsersInRoleAsync(Roles.Agente.ToString()).Result.ToList();
            var allAgents = users.Select(user => new AuthenticationResponse
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                UserName = user.UserName,
                ImageProfile = user.ImageProfile,
                IsVerified = user.EmailConfirmed
            }).ToList();
            return allAgents;
        } 
        public async Task<AuthenticationResponse> GetUserInfo(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            var agent = new AuthenticationResponse();
            agent.Id = user.Id;
            agent.Name = user.Name;
            agent.LastName = user.LastName;
            agent.UserName = user.UserName;
            agent.ImageProfile = user.ImageProfile;
            agent.Email = user.Email;
            agent.Phone = user.PhoneNumber;
            agent.IsVerified = user.EmailConfirmed;
            
            return agent;
        }

        public async Task ChangeUserState(string id, string estado)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (estado == "activo" || estado == "true")
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
            }
            else if(estado == "inactivo" || estado == "false")
            {
                user.EmailConfirmed = false;
                await _userManager.UpdateAsync(user);
            }

        }

        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var rolesClaims = new List<Claim>();
            foreach (var role in roles)
            {
                rolesClaims.Add(new Claim("roles", role));
            }
            var claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email,user.Email),
                    new Claim("uid",user.Id),

                }
            .Union(userClaims)
            .Union(rolesClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWTSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jWTSettings.Issuer,
                audience: _jWTSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jWTSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
            };
        }
         private string RandomTokenString()
        {
            using(var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[40];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return BitConverter.ToString(randomBytes).Replace("-","");
            }

        }

    }
}
