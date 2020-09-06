using CDSHooks.Domain;
using System;
using System.Linq;

namespace CDSHooks.Models.Mappings
{
    public static class HookMapping
    {
        public static HookViewModel ToDisplayViewModel(this Hook hook)
        {
            return new HookViewModel
            {
                Id = hook.Id,
                Workflow = hook.Workflow,
                Context = hook.Context?.Select(context => new HookContextViewModel
                {
                    Field = context.Field,
                    Description = context.Description,
                    IsPrefetchToken = context.IsPrefetchToken,
                    IsRequired = context.Optionality == ContextOptionality.REQUIRED,
                    Type = TypeMapping.TypeToDisplay(context.Type)
                }).ToList()
            };
        }

        public static HookViewModel ToEditableViewModel(this Hook hook)
        {
            return new HookViewModel
            {
                Id = hook.Id,
                Workflow = hook.Workflow,
                Context = hook.Context?.Select(context => new HookContextViewModel
                {
                    Field = context.Field,
                    Description = context.Description,
                    IsPrefetchToken = context.IsPrefetchToken,
                    IsRequired = context.Optionality == ContextOptionality.REQUIRED,
                    Type = context.Type.FullName
                }).ToList()
            };
        }

        public static Hook ToEntity(this HookViewModel hookViewModel)
        {
            return new Hook
            {
                Id = hookViewModel.Id,
                Workflow = hookViewModel.Workflow,
                Context = hookViewModel.Context?.Select(context => new HookContext
                {
                    Field = context.Field,
                    Description = context.Description,
                    IsPrefetchToken = context.IsPrefetchToken,
                    Optionality = context.IsRequired ? ContextOptionality.REQUIRED : ContextOptionality.OPTIONAL,
                    Type = Type.GetType(context.Type)
                }).ToList()
            };
        }
    }
}
