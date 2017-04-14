export class TvShow { 
    public id:any;
    public name:string;

    public fromJSON(data: { name:string }): TvShow {
        let tvShow = new TvShow();
        tvShow.name = data.name;
        return tvShow;
    }
}
