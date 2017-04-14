import { Episode } from "./episode.model";

const template = require("./episode-list-embed.component.html");
const styles = require("./episode-list-embed.component.scss");

export class EpisodeListEmbedComponent extends HTMLElement {
    constructor(
        private _document: Document = document
    ) {
        super();
    }


    static get observedAttributes() {
        return [
            "episodes"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
    }

    private async _bind() {        
        for (let i = 0; i < this.episodes.length; i++) {
            let el = this._document.createElement(`ce-episode-item-embed`);
            el.setAttribute("entity", JSON.stringify(this.episodes[i]));
            this.appendChild(el);
        }    
    }

    episodes:Array<Episode> = [];

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "episodes":
                this.episodes = JSON.parse(newValue);
                if (this.parentElement)
                    this.connectedCallback();
                break;
        }
    }
}

customElements.define("ce-episode-list-embed", EpisodeListEmbedComponent);
