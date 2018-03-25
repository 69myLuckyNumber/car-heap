using System.Linq;
using AutoMapper;
using car_heap.Controllers.Resources;
using car_heap.Controllers.Resources.OrderResources;
using car_heap.Controllers.Resources.UserResources;
using car_heap.Controllers.Resources.VehicleResources;
using car_heap.Core.Models;

namespace car_heap.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to API Resources
            CreateMap<Make, MakeResource>()
                .ForMember(ms => ms.Id, opts => opts.MapFrom(m => m.MakeId))
                .ForMember(ms => ms.Name, opts => opts.MapFrom(m => m.Name))
                .ForMember(ms => ms.Models, opts => opts.MapFrom(m => m.Models));
            CreateMap<Model, ModelResource>()
                .ForMember(mds => mds.Id, opts => opts.MapFrom(m => m.ModelId))
                .ForMember(mds => mds.Name, opts => opts.MapFrom(m => m.Name));
            CreateMap<Make, KeyValuePairResource>()
                .ForMember(ms => ms.Id, opts => opts.MapFrom(m => m.MakeId))
                .ForMember(ms => ms.Name, opts => opts.MapFrom(m => m.Name));
            CreateMap<Model, KeyValuePairResource>()
                .ForMember(mds => mds.Id, opts => opts.MapFrom(m => m.ModelId))
                .ForMember(mds => mds.Name, opts => opts.MapFrom(m => m.Name));
            CreateMap<Feature, FeatureResource>()
                .ForMember(fs => fs.Id, opts => opts.MapFrom(f => f.FeatureId))
                .ForMember(fs => fs.Name, opts => opts.MapFrom(f => f.Name))
                .ForMember(fs => fs.Description, opts => opts.MapFrom(f => f.Description));
            CreateMap<Status, KeyValuePairResource>()
                .ForMember(s => s.Id, opts => opts.MapFrom(s => s.StatusId))
                .ForMember(s => s.Name, opts => opts.MapFrom(s => s.Name));
            CreateMap<Order, PlainOrderResource>()
                .ForMember(os => os.Vehicle, opts => opts.MapFrom(o => new PlainVehicleResource
                {
                    Id = o.VehicleId,
                        Name = o.Vehicle.Name,
                        IsRegistered = o.Vehicle.IsRegistered,
                        LastUpdated = o.Vehicle.LastUpdated,
                        Make = new KeyValuePairResource { Id = o.Vehicle.Model.MakeId, Name = o.Vehicle.Model.Make.Name },
                        Model = new KeyValuePairResource { Id = o.Vehicle.ModelId, Name = o.Vehicle.Model.Name }
                }))
                .ForMember(os => os.User, opts => opts.MapFrom(o => new PlainUserResource
                {
                    Id = o.IdentityId,
                        UserName = o.Identity.UserName,
                        DateRegistered = o.Identity.DateRegistered,
                        Contact = new ContactResource
                        {
                            Id = o.Identity.Contact.ContactId,
                                FirstName = o.Identity.Contact.FirstName,
                                LastName = o.Identity.Contact.LastName,
                                Phone = o.Identity.Contact.Phone
                        }
                }))
                .ForMember(os => os.Status, opts =>
                    opts.MapFrom(o => new KeyValuePairResource { Id = o.StatusId, Name = o.Status.Name }));
            CreateMap<ApplicationUser, PlainUserResource>()
                .ForMember(us => us.Id, opts => opts.MapFrom(u => u.Id))
                .ForMember(us => us.UserName, opts => opts.MapFrom(u => u.UserName))
                .ForMember(us => us.DateRegistered, opts => opts.MapFrom(u => u.DateRegistered))
                .ForMember(us => us.Contact, opts =>
                    opts.MapFrom(u => new ContactResource
                    {
                        Id = u.Contact.ContactId, FirstName = u.Contact.FirstName,
                            LastName = u.Contact.LastName, Phone = u.Contact.Phone
                    }));
            CreateMap<Vehicle, VehicleResource>()
                .ForMember(vr => vr.Id, opts => opts.MapFrom(v => v.VehicleId))
                .ForMember(vr => vr.IsRegistered, opts => opts.MapFrom(v => v.IsRegistered))
                .ForMember(vr => vr.LastUpdated, opts => opts.MapFrom(v => v.LastUpdated))
                .ForMember(vr => vr.Make, opts =>
                    opts.MapFrom(v => new KeyValuePairResource { Id = v.Model.MakeId, Name = v.Model.Make.Name }))
                .ForMember(vr => vr.Model, opts =>
                    opts.MapFrom(v => new KeyValuePairResource { Id = v.ModelId, Name = v.Model.Name }))
                .ForMember(vr => vr.Name, opts => opts.MapFrom(v => v.Name))
                .ForMember(vr => vr.Orders, opts => opts.MapFrom(v =>
                    v.Orders.Select(o => new PlainOrderResource
                    {
                        Vehicle = new PlainVehicleResource
                            {
                                Id = o.VehicleId,
                                    Name = o.Vehicle.Name,
                                    IsRegistered = o.Vehicle.IsRegistered,
                                    Model = new KeyValuePairResource { Id = o.Vehicle.ModelId, Name = o.Vehicle.Model.Name },
                                    Make = new KeyValuePairResource { Id = o.Vehicle.Model.MakeId, Name = o.Vehicle.Model.Make.Name }
                            },
                            User = new PlainUserResource
                            {
                                Id = o.Identity.Id,
                                    UserName = o.Identity.UserName,
                                    DateRegistered = o.Identity.DateRegistered,
                                    Contact = new ContactResource
                                    {
                                        Id = v.Identity.Contact.ContactId,
                                            FirstName = v.Identity.Contact.FirstName,
                                            LastName = v.Identity.Contact.LastName,
                                            Phone = v.Identity.Contact.Phone
                                    }
                            },
                            Comment = o.Comment, DateExpired = o.DateExpired, DateRequested = o.DateRequested,
                            Status = new KeyValuePairResource { Id = o.StatusId, Name = o.Status.Name }
                    })))
                .ForMember(vr => vr.User, opts => opts.MapFrom(v => new PlainUserResource
                {
                    Id = v.IdentityId,
                        UserName = v.Identity.UserName,
                        DateRegistered = v.Identity.DateRegistered,
                        Contact = new ContactResource
                        {
                            Id = v.Identity.Contact.ContactId,
                                FirstName = v.Identity.Contact.FirstName,
                                LastName = v.Identity.Contact.LastName,
                                Phone = v.Identity.Contact.Phone
                        }
                }))
                .ForMember(vr => vr.Features, opts =>
                    opts.MapFrom(v => v.Features.Select(f =>
                        new FeatureResource
                        {
                            Id = f.FeatureId,
                                Name = f.Feature.Name,
                                Description = f.Feature.Description
                        })));
            
            CreateMap<Vehicle, SaveVehicleResource>()
                .ForMember(sv => sv.Id, opts => opts.Ignore())
                .ForMember(sv => sv.IdentityId, opts => opts.MapFrom(v => v.IdentityId))
                .ForMember(sv => sv.ModelId, opts => opts.MapFrom(v => v.ModelId))
                .ForMember(sv => sv.IsRegistered, opts => opts.MapFrom(v => v.IsRegistered))
                .ForMember(sv => sv.Name, opts => opts.MapFrom(v => v.Name))
                .ForMember(sv => sv.Features, opts => opts.MapFrom(v => v.Features.Select(f => f.FeatureId)));

            CreateMap<Order, SaveOrderResource>()
                .ForMember(s => s.IdentityId, opts => opts.MapFrom(o => o.IdentityId))
                .ForMember(s => s.StatusId, opts => opts.MapFrom(o => o.StatusId))
                .ForMember(s => s.VehicleId, opts => opts.MapFrom(o => o.VehicleId))
                .ForMember(s => s.Comment, opts => opts.MapFrom(o => o.Comment))
                .ForMember(s => s.DateRequested, opts => opts.MapFrom(o => o.DateRequested))
                .ForMember(s => s.DateExpired, opts => opts.MapFrom(o => o.DateExpired));

            // API Resources to Domain
            CreateMap<SaveVehicleResource, Vehicle>()
                .ForMember(v => v.VehicleId, opts => opts.Ignore())
                .ForMember(v => v.IdentityId, opts => opts.MapFrom(vr => vr.IdentityId))
                .ForMember(v => v.ModelId, opts => opts.MapFrom(vr => vr.ModelId))
                .ForMember(v => v.Name, opts => opts.MapFrom(vr => vr.Name))
                .ForMember(v => v.IsRegistered, opts => opts.MapFrom(vr => vr.IsRegistered))
                .ForMember(v => v.Features, opts => opts.Ignore())
                .AfterMap((vr, v)=>
                {
                    // delete features
                    var removed = v.Features.Where(f => !vr.Features.Contains(f.FeatureId)).ToList();
                    removed.ForEach(f => v.Features.Remove(f));

                    // add features
                    var added = vr.Features.Where(id => !v.Features.Any(f => f.FeatureId == id))
                        .Select(id => new Integration { FeatureId = id })
                        .ToList();
                    added.ForEach(i => v.Features.Add(i));
                });

            CreateMap<SaveUserResource, ApplicationUser>()
                .ForMember(u => u.UserName, opts => opts.MapFrom(su => su.UserName ?? su.Email))
                .ForMember(u => u.Email, opts => opts.MapFrom(su => su.Email))
                .ForMember(u => u.Contact, opts => opts.MapFrom(su => new Contact
                {
                    Phone = su.Phone,
                        FirstName = su.FirstName,
                        LastName = su.LastName
                }));
            CreateMap<SaveOrderResource, Order>()
                .ForMember(o => o.IdentityId, opts => opts.MapFrom(os => os.IdentityId))
                .ForMember(o => o.VehicleId, opts => opts.MapFrom(os => os.VehicleId))
                .ForMember(o => o.StatusId, opts => opts.MapFrom(os => os.StatusId))
                .ForMember(o => o.Comment, opts => opts.MapFrom(os => os.Comment))
                .ForMember(o => o.DateRequested, opts => opts.MapFrom(os => os.DateRequested))
                .ForMember(o => o.DateExpired, opts => opts.MapFrom(os => os.DateExpired));
        }
    }
}