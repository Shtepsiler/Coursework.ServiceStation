using AutoMapper;
using PARTS.BLL.DTOs.Requests;
using PARTS.BLL.DTOs.Responses;
using PARTS.DAL.Entities.Item;
using PARTS.DAL.Entities.Vehicle;

namespace PARTS.BLL.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateBrandMap();
            CreateCategoryImagerMap();
            CreateCategotyMap();
            CreateEngineMap();
            CreateMakeMap();
            CreateModelMap();
            CreatePartImageMap();
            CreatePartMap();
            CreateSubModelMap();
            CreateVehicleMap();


        }


        private void CreateBrandMap()
        {
            CreateMap<Brand, BrandRequest>().ReverseMap();
            CreateMap<Brand, BrandResponse>().ReverseMap();

        }
        private void CreateCategoryImagerMap()
        {
            CreateMap<CategoryImage, CategoryImageRequest>().ReverseMap();
            CreateMap<CategoryImage, CategoryImageResponse>().ReverseMap();

        }


        private void CreateCategotyMap()
        {
            CreateMap<Category, CategoryRequest>().ReverseMap();
            CreateMap<Category, CategoryResponse>().ReverseMap();

        }

        private void CreateEngineMap()
        {
            CreateMap<Engine, EngineRequest>().ReverseMap();
            CreateMap<Engine, EngineResponse>().ReverseMap();

        }

        private void CreateMakeMap()
        {
            CreateMap<Make, MakeRequest>().ReverseMap();
            CreateMap<Make, MakeResponse>().ReverseMap();

        }

        private void CreateModelMap()
        {
            CreateMap<Model, ModelRequest>().ReverseMap();
            CreateMap<Model, ModelResponse>().ReverseMap();

        }

        private void CreatePartImageMap()
        {
            CreateMap<PartImage, PartImageRequest>().ReverseMap();
            CreateMap<PartImage, PartImageResponse>().ReverseMap();

        }

        private void CreatePartMap()
        {
            CreateMap<Part, PartRequest>().ReverseMap();
            CreateMap<Part, PartResponse>().ReverseMap();

        }

        private void CreateSubModelMap()
        {
            CreateMap<SubModel, SubModelRequest>().ReverseMap();
            CreateMap<SubModel, SubModelResponse>().ReverseMap();

        }

        private void CreateVehicleMap()
        {
            CreateMap<Vehicle, VehicleRequest>().ReverseMap();
            CreateMap<Vehicle, VehicleResponse>().ReverseMap();

        }








        /*



                private void CreateJobMap()
                {
                    CreateMap<Job, ClientsJobsResponse>()
                        .ForMember(r => r.ManagerName, opt => opt.MapFrom(manager => $"{manager.Manager.FullName}"))
                        .ForMember(r => r.ManagerPhone, opt => opt.MapFrom(manager => $"{manager.Manager.PhoneNumber}"))
                        .ForMember(r => r.MechanicFullName, opt => opt.MapFrom(mech => $"{mech.Mechanic.FirstName} {mech.Mechanic.LastName}"))
                        .ForMember(r => r.Model, opt => opt.MapFrom(model => model.Model.Name))
                        ;
                    CreateMap<Job, JobResponse>().ReverseMap();
                    CreateMap<JobRequest, Job>().ReverseMap();
                }

                private void CreateManagerMap()
                {
                    CreateMap<Manager, ManagerResponse>();
                    CreateMap<ManagerRequest, Manager>();


                }
                private void CreateMechanicMap()
                {
                    CreateMap<Mechanic, MechanicResponse>().ReverseMap();
                    CreateMap<Mechanic, MechanicPublicResponse>().ReverseMap();

                }
                private void CreateModelMap()
                {
                    CreateMap<Model, ModelResponse>();
                    CreateMap<ModelRequest, Model>();
                }
                private void CreateIdentityMap()
                {
                    CreateMap<Client, ClientSignUpRequest>().ReverseMap();
                }
                private void CreateClientMap()
                {
                    CreateMap<Client, ClientResponse>().ForMember(r => r.ClientName, opt => opt.MapFrom(client => client.UserName)).ReverseMap();
                    CreateMap<ClientRequest, Client>().ForMember(r => r.UserName, opt => opt.MapFrom(client => client.ClientName)).ReverseMap();

                }



        */
        /*
                private void CreateClientMaps()
                {
                    CreateMap<UserSignUpRequest, User>();
                    CreateMap<UserRequest, User>();
                    CreateMap<User, UserResponse>()
                        .ForMember(
                            response => response.FullName,
                            options => options.MapFrom(user => $"{user.FirstName} {user.LastName}"))
                        .ForMember(
                            response => response.Avatar,
                            options => options.MapFrom(
                                user => !string.IsNullOrWhiteSpace(user.Avatar) ? $"Public/Photos/{user.Avatar}" : null));
                }

                private void CreateTicketsMaps()
                {
                    CreateMap<TicketRequest, Ticket>();
                    CreateMap<Ticket, TicketResponse>();
                }

                private void CreateTeamsMaps()
                {
                    CreateMap<TeamRequest, Team>();
                    CreateMap<Team, TeamResponse>()
                        .ForMember(
                            response => response.Avatar,
                            options => options.MapFrom(
                                team => !string.IsNullOrWhiteSpace(team.Avatar) ? $"Public/Photos/{team.Avatar}" : null));
                }

                private void CreateProjectsMaps()
                {
                    CreateMap<ProjectRequest, Project>();
                    CreateMap<Project, ProjectResponse>();
                }

                private void CreateRatingsMaps()
                {
                    CreateMap<RatingRequest, Rating>();
                    CreateMap<Rating, RatingResponse>().ForMember(
                        response => response.Average,
                        options => options.MapFrom(
                            rating => (rating.Skills
                                       + rating.Social
                                       + rating.Punctuality
                                       + rating.Responsibility) / 4));
                }
        */

    }
}
