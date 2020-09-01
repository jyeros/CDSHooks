using CDSHooks.Domain;
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
                    },
                    new Hook
                    {
                        Id = "medication-prescribe",
                        Workflow = "The user is in the process of prescribing one or more new medications.",
                        Context = new List<HookContext>
                        {
                            new HookContext
                            {
                                Field = "userId",
                                Optionality =  ContextOptionality.REQUIRED,
                                IsPrefetchToken = true,
                                Type = "string",
                                Description = "The id of the current user. For this hook, the user is expected to be of type Practitioner. For example, Practitioner/123"
                            },
                            new HookContext
                            {
                                Field = "patientId",
                                Optionality =  ContextOptionality.REQUIRED,
                                IsPrefetchToken = true,
                                Type = "string",
                                Description = "The FHIR Patient.id of the current patient in context"
                            },
                            new HookContext
                            {
                                Field = "encounterId",
                                Optionality =  ContextOptionality.OPTIONAL,
                                IsPrefetchToken = true,
                                Type = "string",
                                Description = "The FHIR Encounter.id of the current encounter in context"
                            },
                            new HookContext
                            {
                                Field = "medications",
                                Optionality =  ContextOptionality.REQUIRED,
                                IsPrefetchToken = false,
                                Type = "Bundle",
                                Description = "DSTU2 - FHIR Bundle of draft MedicationOrder resources. STU3 - FHIR Bundle of draft MedicationRequest resources"
                            }
                        }
                    },
                    new Hook
                    {
                        Id = "order-review",
                        Workflow = "The user is in the process of reviewing a set of orders to sign.",
                        Context = new List<HookContext>
                        {
                            new HookContext
                            {
                                Field = "userId",
                                Optionality =  ContextOptionality.REQUIRED,
                                IsPrefetchToken = true,
                                Type = "string",
                                Description = "The id of the current user. For this hook, the user is expected to be of type Practitioner. For example, Practitioner/123"
                            },
                            new HookContext
                            {
                                Field = "patientId",
                                Optionality =  ContextOptionality.REQUIRED,
                                IsPrefetchToken = true,
                                Type = "string",
                                Description = "The FHIR Patient.id of the current patient in context"
                            },
                            new HookContext
                            {
                                Field = "encounterId",
                                Optionality =  ContextOptionality.OPTIONAL,
                                IsPrefetchToken = true,
                                Type = "string",
                                Description = "The FHIR Encounter.id of the current encounter in context"
                            },
                            new HookContext
                            {
                                Field = "orders",
                                Optionality =  ContextOptionality.REQUIRED,
                                IsPrefetchToken = false,
                                Type = "Bundle",
                                Description = "DSTU2 - FHIR Bundle of MedicationOrder, DiagnosticOrder, DeviceUseRequest, ReferralRequest, ProcedureRequest, NutritionOrder, VisionPrescription with draft status. STU3 - FHIR Bundle of MedicationRequest, ReferralRequest, ProcedureRequest, NutritionOrder, VisionPrescription with draft status"
                            }
                        }
                    },
                    new Hook
                    {
                        Id = "order-select",
                        Workflow = "The order-select hook fires when a clinician selects one or more orders to place for a patient, (including orders for medications, procedures, labs and other orders). If supported by the CDS Client, this hook may also be invoked each time the clinician selects a detail regarding the order. This hook is among the first workflow events for an order entering a draft status. The context of this hook may include defaulted order details as it first occurs immediately upon the clinician selecting the order from the order catalogue of the CPOE, or upon her manual selection of order details (e.g. dose, quantity, route, etc). CDS services should expect some of the order information to not yet be specified. Additionally, the context may include previously selected orders that are not yet signed from the same ordering session. The order-select hook occurs after the clinician selects the order and before signing. This hook is intended to replace (deprecate) the medication-prescribe hook.",
                        Context = new List<HookContext>
                        {
                            new HookContext
                            {
                                Field = "userId",
                                Optionality =  ContextOptionality.REQUIRED,
                                IsPrefetchToken = true,
                                Type = "string",
                                Description = "The id of the current user. For this hook, the user is expected to be of type Practitioner or PractitionerRole. For example, PractitionerRole/123 or Practitioner/abc."
                            },
                            new HookContext
                            {
                                Field = "patientId",
                                Optionality =  ContextOptionality.REQUIRED,
                                IsPrefetchToken = true,
                                Type = "string",
                                Description = "The FHIR Patient.id of the current patient in context"
                            },
                            new HookContext
                            {
                                Field = "encounterId",
                                Optionality =  ContextOptionality.OPTIONAL,
                                IsPrefetchToken = true,
                                Type = "string",
                                Description = "The FHIR Encounter.id of the current encounter in context"
                            },
                            new HookContext
                            {
                                Field = "selections",
                                Optionality =  ContextOptionality.REQUIRED,
                                IsPrefetchToken = false,
                                Type = "Reference[]",
                                Description = "The FHIR id of the newly selected order(s). The selections field references FHIR resources in the draftOrders Bundle. For example, MedicationRequest/103."
                            },
                            new HookContext
                            {
                                Field = "draftOrders",
                                Optionality =  ContextOptionality.REQUIRED,
                                IsPrefetchToken = false,
                                Type = "Bundle",
                                Description = "DSTU2 - FHIR Bundle of MedicationOrder, DiagnosticOrder, DeviceUseRequest, ReferralRequest, ProcedureRequest, NutritionOrder, VisionPrescription with draft status. STU3 - FHIR Bundle of MedicationRequest, ReferralRequest, ProcedureRequest, NutritionOrder, VisionPrescription with draft status. R4 - FHIR Bundle of MedicationRequest, NutritionOrder, ServiceRequest, VisionPrescription with draft status"
                            }
                        }
                    },
                    new Hook
                    {
                        Id = "order-sign",
                        Workflow = "The order-sign hook fires when a clinician is ready to sign one or more orders for a patient, (including orders for medications, procedures, labs and other orders). This hook is among the last workflow events before an order is promoted out of a draft status. The context contains all order details, such as dose, quantity, route, etc, although the order has not yet been signed and therefore still exists in a draft status. Use this hook when your service requires all order details, and the clinician will accept recommended changes. This hook is intended to replace (deprecate) the medication-prescribe and order-review hooks.",
                        Context = new List<HookContext>
                        {
                            new HookContext
                            {
                                Field = "userId",
                                Optionality =  ContextOptionality.REQUIRED,
                                IsPrefetchToken = true,
                                Type = "string",
                                Description = "The id of the current user. For this hook, the user is expected to be of type Practitioner or PractitionerRole. For example, PractitionerRole/123 or Practitioner/abc."
                            },
                            new HookContext
                            {
                                Field = "patientId",
                                Optionality =  ContextOptionality.REQUIRED,
                                IsPrefetchToken = true,
                                Type = "string",
                                Description = "The FHIR Patient.id of the current patient in context"
                            },
                            new HookContext
                            {
                                Field = "encounterId",
                                Optionality =  ContextOptionality.OPTIONAL,
                                IsPrefetchToken = true,
                                Type = "string",
                                Description = "The FHIR Encounter.id of the current encounter in context"
                            },
                            new HookContext
                            {
                                Field = "selections",
                                Optionality =  ContextOptionality.REQUIRED,
                                IsPrefetchToken = false,
                                Type = "Bundle",
                                Description = "DSTU2 - FHIR Bundle of MedicationOrder, DiagnosticOrder, DeviceUseRequest, ReferralRequest, ProcedureRequest, NutritionOrder, VisionPrescription with draft status. STU3 - FHIR Bundle of MedicationRequest, ReferralRequest, ProcedureRequest, NutritionOrder, VisionPrescription with draft status. R4 - FHIR Bundle of MedicationRequest, NutritionOrder, ServiceRequest, VisionPrescription with draft status"
                            }
                        }
                    },
                    new Hook
                    {
                        Id = "appointment-book",
                        Workflow = "This hook is invoked when the user is scheduling one or more future encounters/visits for the patient. For example, the appointment-book hook may be triggered for an appointment with the appointment creator, a clinician within the same organization as the appointment creator or even for an appointment outside the creator's organization. It may be invoked at the start and end of the booking process and/or any time between those two points. This hook enables CDS Services to intervene in the decision of when future appointments should be scheduled, where they should be scheduled, what services should be booked, to identify actions that need to occur prior to scheduled appointments, etc.",
                        Context = new List<HookContext>
                        {
                            new HookContext
                            {
                                Field = "userId",
                                Optionality =  ContextOptionality.REQUIRED,
                                IsPrefetchToken = true,
                                Type = "string",
                                Description = "The id of the current user. For this hook, the user could be of type Practitioner, PractitionerRole, Patient, or RelatedPerson. For example, PractitionerRole/123. Patient or RelatedPerson are appropriate when a patient or their proxy are booking the appointment."
                            },
                            new HookContext
                            {
                                Field = "patientId",
                                Optionality =  ContextOptionality.REQUIRED,
                                IsPrefetchToken = true,
                                Type = "string",
                                Description = "The FHIR Patient.id of Patient appointment(s) is/are for"
                            },
                            new HookContext
                            {
                                Field = "encounterId",
                                Optionality =  ContextOptionality.OPTIONAL,
                                IsPrefetchToken = true,
                                Type = "string",
                                Description = "The FHIR Encounter.id of Encounter where booking was initiated"
                            },
                            new HookContext
                            {
                                Field = "appointments",
                                Optionality =  ContextOptionality.REQUIRED,
                                IsPrefetchToken = false,
                                Type = "Bundle",
                                Description = "DSTU2/STU3/R4 - FHIR Bundle of Appointments in 'proposed' state"
                            }
                        }
                    },
                    new Hook
                    {
                        Id = "encounter-start",
                        Workflow = "This hook is invoked when the user is initiating a new encounter. In an inpatient setting, this would be the time of admission. In an outpatient/community environment, this would be the time of patient-check-in for a face-to-face or equivalent for a virtual/telephone encounter. The Encounter should either be in one of the following states: planned | arrived | triaged | in-progress. Note that there can be multiple 'starts' for the same encounter as each user becomes engaged. For example, when a scheduled encounter is presented at the beginning of the day for planning purposes, when the patient arrives, when the patient first encounters a clinician, etc. Hooks may present different information depending on user role and Encounter.status. Note: This is distinct from the patient-view hook which occurs any time the patient's record is looked at - which might be done outside the context of any encounter and will often occur during workflows that are not linked to the initiation of an encounter. The intention is that the cards from any invoked CDS Services are available at the time when decisions are being made about what actions are going to occur during this encounter. For example, identifying that the patient is due for certain diagnostic tests or interventions, identifying additional information that should be collected to comply with protocols associated with clinical studies the patient is enrolled in, identifying any documentation or other requirements associated with patient insurance, etc.",
                        Context = new List<HookContext>
                        {
                            new HookContext
                            {
                                Field = "userId",
                                Optionality =  ContextOptionality.REQUIRED,
                                IsPrefetchToken = true,
                                Type = "string",
                                Description = "The id of the current user. For this hook, the user is expected to be of type Practitioner or PractitionerRole. For example, PractitionerRole/123"
                            },
                            new HookContext
                            {
                                Field = "patientId",
                                Optionality =  ContextOptionality.REQUIRED,
                                IsPrefetchToken = true,
                                Type = "string",
                                Description = "The FHIR Patient.id of the Patient the Encounter is for"
                            },
                            new HookContext
                            {
                                Field = "encounterId",
                                Optionality =  ContextOptionality.REQUIRED,
                                IsPrefetchToken = true,
                                Type = "string",
                                Description = "The FHIR Encounter.id of the Encounter being started"
                            }
                        }
                    },
                    new Hook
                    {
                        Id = "encounter-discharge",
                        Workflow = "This hook is invoked when the user is performing the discharge process for an encounter where the notion of 'discharge' is relevant - typically an inpatient encounter. It may be invoked at the start and end of the discharge process or any time between those two points. It allows hook services to intervene in the decision of whether discharge is appropriate, to verify discharge medications, to check for continuity of care planning, to ensure necessary documentation is present for discharge processing, etc.",
                        Context = new List<HookContext>
                        {
                            new HookContext
                            {
                                Field = "userId",
                                Optionality =  ContextOptionality.REQUIRED,
                                IsPrefetchToken = true,
                                Type = "string",
                                Description = "The id of the current user. For this hook, the user is expected to be of type Practitioner or PractitionerRole. For example, Practitioner/123"
                            },
                            new HookContext
                            {
                                Field = "patientId",
                                Optionality =  ContextOptionality.REQUIRED,
                                IsPrefetchToken = true,
                                Type = "string",
                                Description = "The FHIR Patient.id of the being discharged"
                            },
                            new HookContext
                            {
                                Field = "encounterId",
                                Optionality =  ContextOptionality.REQUIRED,
                                IsPrefetchToken = true,
                                Type = "string",
                                Description = "The FHIR Encounter.id of the Encounter being ended"
                            }
                        }
                    }
            };
        }

        public static List<CDSService> GetServices()
        {
            return new List<CDSService>
            {
                new CDSService
                {
                    Id = "patient-greeting",
                    Title = "Patient greeting",
                    HookId = "patient-view",
                    CodeType = CDSServiceCodeType.JSON,
                    Code = "{\"cards\":[{\"uuid\":\"12345678-1234-1234-1234-123456789012\",\"summary\":\"NowSeeing\",\"source\":{\"label\":\"PatientGreetingService\"},\"indicator\":\"info\"}]}",
                    Description = "Display generic patient grettings"
                }
            };
        }
    }
}
