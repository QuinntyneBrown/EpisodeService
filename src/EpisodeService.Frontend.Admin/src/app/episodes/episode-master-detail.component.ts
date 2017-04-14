import { EpisodeAdd, EpisodeDelete, EpisodeEdit, episodeActions } from "./episode.actions";
import { Episode } from "./episode.model";
import { EpisodeService } from "./episode.service";

const template = require("./episode-master-detail.component.html");
const styles = require("./episode-master-detail.component.scss");

export class EpisodeMasterDetailComponent extends HTMLElement {
    constructor(
        private _episodeService: EpisodeService = EpisodeService.Instance	
	) {
        super();
        this.onEpisodeAdd = this.onEpisodeAdd.bind(this);
        this.onEpisodeEdit = this.onEpisodeEdit.bind(this);
        this.onEpisodeDelete = this.onEpisodeDelete.bind(this);
    }

    static get observedAttributes () {
        return [
            "episodes"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.episodes = await this._episodeService.get();
        this.episodeListElement.setAttribute("episodes", JSON.stringify(this.episodes));
    }

    private _setEventListeners() {
        this.addEventListener(episodeActions.ADD, this.onEpisodeAdd);
        this.addEventListener(episodeActions.EDIT, this.onEpisodeEdit);
        this.addEventListener(episodeActions.DELETE, this.onEpisodeDelete);
    }

    disconnectedCallback() {
        this.removeEventListener(episodeActions.ADD, this.onEpisodeAdd);
        this.removeEventListener(episodeActions.EDIT, this.onEpisodeEdit);
        this.removeEventListener(episodeActions.DELETE, this.onEpisodeDelete);
    }

    public async onEpisodeAdd(e) {

        await this._episodeService.add(e.detail.episode);
        this.episodes = await this._episodeService.get();
        
        this.episodeListElement.setAttribute("episodes", JSON.stringify(this.episodes));
        this.episodeEditElement.setAttribute("episode", JSON.stringify(new Episode()));
    }

    public onEpisodeEdit(e) {
        this.episodeEditElement.setAttribute("episode", JSON.stringify(e.detail.episode));
    }

    public async onEpisodeDelete(e) {

        await this._episodeService.remove(e.detail.episode.id);
        this.episodes = await this._episodeService.get();
        
        this.episodeListElement.setAttribute("episodes", JSON.stringify(this.episodes));
        this.episodeEditElement.setAttribute("episode", JSON.stringify(new Episode()));
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "episodes":
                this.episodes = JSON.parse(newValue);

                if (this.parentNode)
                    this.connectedCallback();

                break;
        }
    }

    public get value(): Array<Episode> { return this.episodes; }

    private episodes: Array<Episode> = [];
    public episode: Episode = <Episode>{};
    public get episodeEditElement(): HTMLElement { return this.querySelector("ce-episode-edit-embed") as HTMLElement; }
    public get episodeListElement(): HTMLElement { return this.querySelector("ce-episode-list-embed") as HTMLElement; }
}

customElements.define(`ce-episode-master-detail`,EpisodeMasterDetailComponent);
