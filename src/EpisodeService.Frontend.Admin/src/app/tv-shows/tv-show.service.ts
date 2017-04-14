import { fetch } from "../utilities";
import { TvShow } from "./tv-show.model";

export class TvShowService {
    constructor(private _fetch = fetch) { }

    private static _instance: TvShowService;

    public static get Instance() {
        this._instance = this._instance || new TvShowService();
        return this._instance;
    }

    public get(): Promise<Array<TvShow>> {
        return this._fetch({ url: "/api/tvshow/get", authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { tvShows: Array<TvShow> }).tvShows;
        });
    }

    public getById(id): Promise<TvShow> {
        return this._fetch({ url: `/api/tvshow/getbyid?id=${id}`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { tvShow: TvShow }).tvShow;
        });
    }

    public add(tvShow) {
        return this._fetch({ url: `/api/tvshow/add`, method: "POST", data: { tvShow }, authRequired: true  });
    }

    public remove(options: { id : number }) {
        return this._fetch({ url: `/api/tvshow/remove?id=${options.id}`, method: "DELETE", authRequired: true  });
    }
    
}
