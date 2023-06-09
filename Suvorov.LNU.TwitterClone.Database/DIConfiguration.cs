﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Suvorov.LNU.TwitterClone.Database.Services;
using Suvorov.LNU.TwitterClone.Database.Interfaces;

namespace Suvorov.LNU.TwitterClone.Database
{
    public static class DIConfiguration
    {
        public static void RegisterDatabseDependencies(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddDbContext<NetworkDbContext>((x) => x.UseSqlServer(configuration.GetConnectionString("NetworkConnection")));

            services.AddScoped(typeof(IDbEntityService<>), typeof(DbEntityService<>));
            services.AddScoped<UserService, UserService>();
            services.AddScoped<PostService, PostService>();
            services.AddScoped<PostTagService, PostTagService>();
            services.AddScoped<PostTagCountService, PostTagCountService>();
            services.AddScoped<LikeService, LikeService>();
            services.AddScoped<FollowService, FollowService>();
            services.AddScoped<FolloweeService, FolloweeService>();
            services.AddScoped<IPostRecommendations, PostRecommendations>();
        }
    }
}
