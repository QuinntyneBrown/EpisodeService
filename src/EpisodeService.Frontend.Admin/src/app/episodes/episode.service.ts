import { fetch } from "../utilities";
import { Episode } from "./episode.model";

export class EpisodeService {
    constructor(private _fetch = fetch) { }

    private static _instance: EpisodeService;

    public static get Instance() {
        this._instance = this._instance || new EpisodeService();
        return this._instance;
    }

    public get(): Promise<Array<Episode>> {
        return this._fetch({ url: "/api/episode/get", authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { episodes: Array<Episode> }).episodes;
        });
    }

    public getById(id): Promise<Episode> {
        return this._fetch({ url: `/api/episode/getbyid?id=${id}`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { episode: Episode }).episode;
        });
    }

    public add(episode) {
        return this._fetch({ url: `/api/episode/add`, method: "POST", data: { episode }, authRequired: true  });
    }

    public remove(options: { id : number }) {
        return this._fetch({ url: `/api/episode/remove?id=${options.id}`, method: "DELETE", authRequired: true  });
    }
    
}
