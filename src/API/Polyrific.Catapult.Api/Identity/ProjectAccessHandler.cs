﻿// Copyright (c) Polyrific, Inc 2018. All rights reserved.

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Polyrific.Catapult.Shared.Dto.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polyrific.Catapult.Api.Identity
{
    public class ProjectAccessHandler : AuthorizationHandler<ProjectAccessRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ProjectAccessRequirement requirement)
        {
            if (context.User.IsInRole(UserRole.Administrator))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            if (!(context.Resource is AuthorizationFilterContext mvcContext))
                return Task.CompletedTask;
                
            var expectedProjectId = Convert.ToInt32(mvcContext.RouteData.Values["projectId"]);
            var expectedProjectName = mvcContext.RouteData.Values["projectName"]?.ToString();

            if (!context.User.HasClaim(c => c.Type == CustomClaimTypes.Projects))
                return Task.CompletedTask;

            var projectsClaim = context.User.FindFirst(c => c.Type == CustomClaimTypes.Projects).Value;
            if (string.IsNullOrEmpty(projectsClaim))
                return Task.CompletedTask;
            
            var userProjects = JsonConvert.DeserializeObject<List<ProjectClaim>>(projectsClaim);

            ProjectClaim projectClaim = null;
            if (expectedProjectId > 0)
            {
                projectClaim = userProjects.FirstOrDefault(up => up.ProjectId == expectedProjectId);
            }
            else if (!string.IsNullOrEmpty(expectedProjectName))
            {
                projectClaim = userProjects.FirstOrDefault(up => up.ProjectName?.ToLower() == expectedProjectName.ToLower());
            }
            else
            {
                projectClaim = userProjects.FirstOrDefault(up => requirement.IsAllowedByMemberRole(up.MemberRole));
            }

            if (projectClaim != null)
            {
                if (requirement.IsAllowedByMemberRole(projectClaim.MemberRole))
                    context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
