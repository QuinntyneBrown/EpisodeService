using EpisodeService.Data.Model;

namespace EpisodeService.Features.TvShows
{
    public class TvShowApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }

        public static TModel FromTvShow<TModel>(TvShow tvShow) where
            TModel : TvShowApiModel, new()
        {
            var model = new TModel();
            model.Id = tvShow.Id;
            model.TenantId = tvShow.TenantId;
            model.Name = tvShow.Name;
            return model;
        }

        public static TvShowApiModel FromTvShow(TvShow tvShow)
            => FromTvShow<TvShowApiModel>(tvShow);

    }
}
