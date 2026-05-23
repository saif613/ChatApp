using AutoMapper;

using ChatApp.Application.DTOs.Response;
using ChatApp.Domain.Entities;

namespace ChatApp.Application.Mapping
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageResponse>()

                .ForCtorParam(
                    "Id",
                    opt => opt.MapFrom(
                        src => src.Id))

                .ForCtorParam(
                    "ConversationId",
                    opt => opt.MapFrom(
                        src => src.ConversationId))

                .ForCtorParam(
                    "Content",
                    opt => opt.MapFrom(
                        src => src.Content))

                .ForCtorParam(
                    "SenderId",
                    opt => opt.MapFrom(
                        src => src.SenderId))

                .ForCtorParam(
                    "ReceiverId",
                    opt => opt.MapFrom(
                        src => src.ReceiverId))

                .ForCtorParam(
                    "SentAt",
                    opt => opt.MapFrom(
                        src => src.SentAt))

                .ForCtorParam(
                    "SeenAt",
                    opt => opt.MapFrom(
                        src => src.SeenAt))

                .ForCtorParam(
                    "EditedAt",
                    opt => opt.MapFrom(
                        src => src.EditedAt))

                .ForCtorParam(
                    "IsEdited",
                    opt => opt.MapFrom(
                        src => src.IsEdited))

                .ForCtorParam(
                    "Status",
                    opt => opt.MapFrom(
                        src => src.Status));



            CreateMap<Message,
                UpdateMessageResponse>()

                .ForCtorParam(
                    "Id",
                    opt => opt.MapFrom(
                        src => src.Id))

                .ForCtorParam(
                    "Content",
                    opt => opt.MapFrom(
                        src => src.Content))

                .ForCtorParam(
                    "EditedAt",
                    opt => opt.MapFrom(
                        src => src.EditedAt));
        }
    }
}