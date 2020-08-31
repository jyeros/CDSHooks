using CDSHooks.Data.Models;
using System.Collections.Generic;

namespace CDSHooks.Core
{
    public class Config
    {
        public static List<Hook> GetHooks()
        {
            return new List<Hook>
                {
                    new Hook
                    {
                        Id = "patient-view",
                        Workflow = "The user has just opened a patient's record.",
                        Context = new List<HookContext>
                        {
                            new HookContext
                            {
                                Field = "userId",
                                Optionality =  ContextOptionality.REQUIRED,
                                IsPrefetchToken = true,
                                Type = "string",
                                Description = "The id of the current user.\nFor example, if the user represents a FHIR resource on the given FHIR server, the resource type would be one of Practitioner, PractitionerRole, Patient, or RelatedPerson.\nPatient or RelatedPerson are appropriate when a patient or their proxy are viewing the record."
                            },
                            new HookContext
                            {
                                Field = "patientId",
                                Optionality = ContextOptionality.REQUIRED,
                                IsPrefetchToken = true,
                                Type = "string",
                                Description = "The FHIR Patient.id of the current patient in context"
                            },
                            new HookContext
                            {
                                Field = "encounterId",
                                Optionality = ContextOptionality.OPTIONAL,
                                IsPrefetchToken = true,
                                Type = "string",
                                Description = "The FHIR Encounter.id of the current encounter in context"
                            }
                        }
                    }
            };
        }
    }
}
