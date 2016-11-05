using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TwitterStyleApplication.Domain.Entities;
using TwitterStyleApplication.Services.DTO;
using TwitterStyleApplication.Services.RequestModels;


namespace TwitterStyleApplication.Web.Configuration
{
	/// <summary>
	/// Mapping entity to DTO
	/// </summary>
	public class AutomapperConfiguration
	{
		/// <summary>
		/// Configuration of automapper maps
		/// </summary>
		public static void Load()
		{
			Mapper.Initialize(config =>
			{
				config.CreateMap<RegistrationRequest, ApplicationUser>()
					.ForMember(dest => dest.Email, dto => dto.MapFrom(src => src.Email))
					.ForMember(dest => dest.UserName, dto => dto.MapFrom(src => src.Email));

				config.CreateMap<Tweet, TweetDTO>()
					.ForMember(dest => dest.Message, dto => dto.MapFrom(src => src.MessageData))
					.ForMember(dest => dest.UserId, dto => dto.MapFrom(src => src.Author.Id))
					.ForMember(dest => dest.Id, dto => dto.MapFrom(src => src.Id))
					.ForMember(dest => dest.CreationDate, dto => dto.MapFrom(src => src.DateCreated));
			});
		}
	}
}
