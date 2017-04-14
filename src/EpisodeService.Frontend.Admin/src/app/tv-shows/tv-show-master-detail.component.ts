import { TvShowAdd, TvShowDelete, TvShowEdit, tvShowActions } from "./tv-show.actions";
import { TvShow } from "./tv-show.model";
import { TvShowService } from "./tv-show.service";

const template = require("./tv-show-master-detail.component.html");
const styles = require("./tv-show-master-detail.component.scss");

export class TvShowMasterDetailComponent extends HTMLElement {
    constructor(
        private _tvShowService: TvShowService = TvShowService.Instance	
	) {
        super();
        this.onTvShowAdd = this.onTvShowAdd.bind(this);
        this.onTvShowEdit = this.onTvShowEdit.bind(this);
        this.onTvShowDelete = this.onTvShowDelete.bind(this);
    }

    static get observedAttributes () {
        return [
            "tv-shows"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.tvShows = await this._tvShowService.get();
        this.tvShowListElement.setAttribute("tv-shows", JSON.stringify(this.tvShows));
    }

    private _setEventListeners() {
        this.addEventListener(tvShowActions.ADD, this.onTvShowAdd);
        this.addEventListener(tvShowActions.EDIT, this.onTvShowEdit);
        this.addEventListener(tvShowActions.DELETE, this.onTvShowDelete);
    }

    disconnectedCallback() {
        this.removeEventListener(tvShowActions.ADD, this.onTvShowAdd);
        this.removeEventListener(tvShowActions.EDIT, this.onTvShowEdit);
        this.removeEventListener(tvShowActions.DELETE, this.onTvShowDelete);
    }

    public async onTvShowAdd(e) {

        await this._tvShowService.add(e.detail.tvShow);
        this.tvShows = await this._tvShowService.get();
        
        this.tvShowListElement.setAttribute("tv-shows", JSON.stringify(this.tvShows));
        this.tvShowEditElement.setAttribute("tv-show", JSON.stringify(new TvShow()));
    }

    public onTvShowEdit(e) {
        this.tvShowEditElement.setAttribute("tv-show", JSON.stringify(e.detail.tvShow));
    }

    public async onTvShowDelete(e) {

        await this._tvShowService.remove(e.detail.tvShow.id);
        this.tvShows = await this._tvShowService.get();
        
        this.tvShowListElement.setAttribute("tv-shows", JSON.stringify(this.tvShows));
        this.tvShowEditElement.setAttribute("tv-show", JSON.stringify(new TvShow()));
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "tv-shows":
                this.tvShows = JSON.parse(newValue);

                if (this.parentNode)
                    this.connectedCallback();

                break;
        }
    }

    public get value(): Array<TvShow> { return this.tvShows; }

    private tvShows: Array<TvShow> = [];
    public tvShow: TvShow = <TvShow>{};
    public get tvShowEditElement(): HTMLElement { return this.querySelector("ce-tv-show-edit-embed") as HTMLElement; }
    public get tvShowListElement(): HTMLElement { return this.querySelector("ce-tv-show-list-embed") as HTMLElement; }
}

customElements.define(`ce-tv-show-master-detail`,TvShowMasterDetailComponent);
