using EpisodeService.Data.Model;

namespace EpisodeService.Features.Episodes
{
    public class EpisodeApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }

        public static TModel FromEpisode<TModel>(Episode episode) where
            TModel : EpisodeApiModel, new()
        {
            var model = new TModel();
            model.Id = episode.Id;
            model.TenantId = episode.TenantId;
            model.Name = episode.Name;
            return model;
        }

        public static EpisodeApiModel FromEpisode(Episode episode)
            => FromEpisode<EpisodeApiModel>(episode);

    }
}
