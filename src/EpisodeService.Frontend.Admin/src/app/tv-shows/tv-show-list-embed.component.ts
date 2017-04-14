import { TvShow } from "./tv-show.model";

const template = require("./tv-show-list-embed.component.html");
const styles = require("./tv-show-list-embed.component.scss");

export class TvShowListEmbedComponent extends HTMLElement {
    constructor(
        private _document: Document = document
    ) {
        super();
    }


    static get observedAttributes() {
        return [
            "tv-shows"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
    }

    private async _bind() {        
        for (let i = 0; i < this.tvShows.length; i++) {
            let el = this._document.createElement(`ce-tv-show-item-embed`);
            el.setAttribute("entity", JSON.stringify(this.tvShows[i]));
            this.appendChild(el);
        }    
    }

    tvShows:Array<TvShow> = [];

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "tv-shows":
                this.tvShows = JSON.parse(newValue);
                if (this.parentElement)
                    this.connectedCallback();
                break;
        }
    }
}

customElements.define("ce-tv-show-list-embed", TvShowListEmbedComponent);
