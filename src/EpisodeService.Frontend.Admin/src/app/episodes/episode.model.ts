export class Episode { 
    public id:any;
    public name:string;

    public fromJSON(data: { name:string }): Episode {
        let episode = new Episode();
        episode.name = data.name;
        return episode;
    }
}
