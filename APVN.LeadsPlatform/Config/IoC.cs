using APVN.LeadsPlatform.BusinessLayer;
using APVN.LeadsPlatform.BusinessLayer.IBusiness;
using APVN.LeadsPlatform.BusinessLayer.IBusinessLayer;
using APVN.LeadsPlatform.BusinessLayer.IServiceCache;
using APVN.LeadsPlatform.BusinessLayer.ServiceCache;
using APVN.LeadsPlatform.Caching;
using APVN.LeadsPlatform.DataAccessLayer;
using APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer;
using APVN.LeadsPlatform.Utility;
using APVN.LeadsPlatform.Utility.Database;
using APVN.LeadsPlatform.Utility.Database.Interfaces;
using APVN.LeadsPlatform.Utility.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APVN.LeadsPlatform.Config
{
    public class IoC
    {
        public static void RegisterTypes(IServiceCollection services)
        {
            //IHttpContextAccessor
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            // Cache register
            switch ((CacheType)AppKeys.CacheType)
            {
                case CacheType.Redis:
                    services.AddTransient<ICache, RedisCache>();
                    break;
                default:
                    services.AddTransient<ICache, NoCache>();
                    break;
            }


            // DAL
            services.AddScoped<IUsersDAL, UsersDAL>();
            services.AddScoped<IUserPermissionDAL, UserPermissionDAL>();
            services.AddScoped<IUserRoleDAL, UserRoleDAL>();
            services.AddScoped<ILeadsDAL, LeadsDAL>();
            services.AddScoped<ILeadsExtendsDAL, LeadsExtendsDAL>();
            services.AddScoped<IMakeDAL, MakeDAL>();
            services.AddScoped<IModelDAL, ModelDAL>();
            services.AddScoped<ICityDAL, CityDAL>();
            services.AddScoped<ICampaignDAL, CampaignDAL>();
            services.AddScoped<IBankDAL, BankDAL>();
            services.AddScoped<IDealerLeadDAL, DealerLeadDAL>();
            services.AddScoped<INoteDAL, NoteDAL>();
            services.AddScoped<IHistoryDAL, HistoryDAL>();
            services.AddScoped<ILeadsQuantityForDealerDAL, LeadsQuantityForDealerDAL>();

            // Business layer
            services.AddScoped<IUserBL, UserBL>();
            services.AddScoped<ICheckPermissionBL, CheckPermissionBL>();
            services.AddScoped<ILeadsBL, LeadsBL>();
            services.AddScoped<ILeadsExtendsBL, LeadsExtendsBL>();
            services.AddScoped<IMakeBL, MakeBL>();
            services.AddScoped<IModelBL, ModelBL>();
            services.AddScoped<ICityBL, CityBL>();
            services.AddScoped<ICampaignBL, CampaignBL>();
            services.AddScoped<IBankBL, BankBL>();
            services.AddScoped<INoteBL, NoteBL>();
            services.AddScoped<IDealerLeadBL, DealerLeadBL>();
            services.AddScoped<IHistoryBL, HistoryBL>();
            services.AddScoped<ILeadsQuantityForDealerBL, LeadsQuantityForDealerBL>();

            // Services - Cache
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMakeServiceCache, MakeServiceCache>();
            services.AddScoped<IModelServiceCache, ModelServiceCache>();
            services.AddScoped<ICityServiceCache, CityServiceCache>();
            services.AddScoped<IBankServiceCache, BankServiceCache>();
            services.AddScoped<ICustomAuthenticationAppService, CustomAuthenticationAppService>();
            //Responsitory
            services.AddScoped<IRepository, Repository>();
            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
