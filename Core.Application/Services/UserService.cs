using AutoMapper;
using Core.Application.DTOS.Account;
using Core.Application.Inferfaces.Service;
using Core.Application.ViewModels;
using Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public UserService(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse>Login(LoginViewModel login)
        {
            AuthenticationRequest request = _mapper.Map<AuthenticationRequest>(login);
            AuthenticationResponse response = await _accountService.Authentication(request);
            return response;
        }
        public async Task SignOut()
        {
            await _accountService.SignOut();
        }
        public async Task<RegisterResponse> Regiter(UserSaveViewModel viewModel, string origin)
        {
            RegisterRequest request = _mapper.Map<RegisterRequest>(viewModel);
            var response = await _accountService.Register(request,origin);
            return response;
        }
        public async Task UpdateUser(string Id,UserSaveViewModel viewModel)
        {
            RegisterRequest request = _mapper.Map<RegisterRequest>(viewModel);
            await _accountService.UpdateUser(Id, request);
        }

        public async Task<string> EmailConfirm(string userId, string token)
        {
            return await _accountService.ConfirmEmail(userId,token);
        }
        public async Task<GenericResponse> ForgotPassword(ForgotPasswordViewModel request, string origin)
        {
            ForgotPasswordRequest forgotRequest = _mapper.Map<ForgotPasswordRequest>(request);
            return await _accountService.ForgotPassword(forgotRequest, origin);
        }
        public async Task<GenericResponse> ResetPassword(ResetPasswordViewModel request)
        {
            ResetPasswordRequest resetRequest = _mapper.Map<ResetPasswordRequest>(request);
            return await _accountService.ResetPassword(resetRequest);
        }
        public async Task<List<AgentesViewModel>> GetAllAgents(AgentSearchViewModel vm)
        {
            var agentes  = _accountService.GetAllAgents();
            var listAgents = agentes.Select(agentes => new AgentesViewModel
            {
                Name = agentes.Name,
                LastName = agentes.LastName,
                ImageProfile = agentes.ImageProfile,
                Email = agentes.Email,
                Id = agentes.Id

            }).ToList();
            if (vm.AgentName != null)
            {
                listAgents = listAgents.Where(agent => (agent.Name +" " + agent.LastName).ToLower().Contains((vm.AgentName).ToLower())).ToList();
            }
            return listAgents;
        }
        public async Task<UserSaveViewModel> GetUserInfo(string Id)
        {
            var agente = await _accountService.GetUserInfo(Id);
            return _mapper.Map<UserSaveViewModel>(agente);
        }
    }
}
