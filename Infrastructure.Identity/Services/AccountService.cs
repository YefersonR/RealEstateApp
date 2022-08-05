﻿using Core.Application.DTO_s.Account;
using Core.Application.Enum;
using Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Services
{
    public class AccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<AuthenticationResponse> Authentication(AuthenticationRequest request)
        {
            var response = new AuthenticationResponse();
            var User = await _userManager.FindByNameAsync(request.UserName);
            if(User == null)
            {
                response.HasError = true;
                response.Error = $"No Account Register with {request.UserName}";
                return response;
            }
            var result = await _signInManager.PasswordSignInAsync(User.UserName,request.Password,false,lockoutOnFailure: false);
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
           
            response.Id = User.Id;
            response.Name= User.Name;
            response.LastName = User.LastName;
            response.Email = User.Email;
            response.IsVerified= User.EmailConfirmed;
            var roles = await _userManager.GetRolesAsync(User).ConfigureAwait(false);
            response.Roles = roles.ToList();
            
            return response;
        }
        
        public async Task<GenericResponse> RegisterClient(RegisterRequest request, string origin)
        {
            var response = new GenericResponse();
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

            };

            var result = await _userManager.CreateAsync(user,request.Password);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = "A error occurred trying to register the user.";
                return response;

            }
            await _userManager.AddToRoleAsync(user,Roles.Cliente.ToString());
            var verificationUrl = await SendVerificationEMailUrl(user, origin); 
            // send A email confirmation

            return response;
        }
        
        private async Task<string> SendVerificationEMailUrl(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/EmailConfirm";
            var url = new Uri(string.Concat($"{origin}/",route));
            var verificationUrl = QueryHelpers.AddQueryString(url.ToString(),"userId",user.Id);
            verificationUrl = QueryHelpers.AddQueryString(verificationUrl, "token", code);

            return verificationUrl;
        }

        public  async Task<string> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                return $"Not account registered with this user";
            }
            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user,token);
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
            var route = "User/ResetPassword";
            var url = new Uri(string.Concat($"{origin}/", route));
            var verificationUrl = QueryHelpers.AddQueryString(url.ToString(), "token", code);
            


            return verificationUrl;
        }
        
        public async Task<GenericResponse> ForgotPassword(ForgotPasswordRequest request,string origin)
        {
            var response = new GenericResponse();
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user == null)
            {
                response.HasError = true;
                response.Error = $"No account registered with {request.Email}";
                return response;
            }
            var verigicationUrl = await SendForgotPasswordUrl(user,origin);


            return response;
        }

        public async Task<GenericResponse> ResetPassword(ResetPasswordRequest request)
        {
            var response = new GenericResponse();
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user == null)
            {
                response.HasError = true;
                response.Error = $"No account registered with {request.Email}";
                return response;
            }
            request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _userManager.ResetPasswordAsync(user, request.Token,request.Password);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error= $"An error occurred while reset password";
                return response;
            }

            return response;
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}