import { Episode } from "./episode.model";
import { EditorComponent } from "../shared";
import {  EpisodeDelete, EpisodeEdit, EpisodeAdd } from "./episode.actions";

const template = require("./episode-edit-embed.component.html");
const styles = require("./episode-edit-embed.component.scss");

export class EpisodeEditEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
    }

    static get observedAttributes() {
        return [
            "episode",
            "episode-id"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.episode ? "Edit Episode": "Create Episode";

        if (this.episode) {                
            this._nameInputElement.value = this.episode.name;  
        } else {
            this._deleteButtonElement.style.display = "none";
        }     
    }

    private _setEventListeners() {
        this._saveButtonElement.addEventListener("click", this.onSave);
        this._deleteButtonElement.addEventListener("click", this.onDelete);
    }

    private disconnectedCallback() {
        this._saveButtonElement.removeEventListener("click", this.onSave);
        this._deleteButtonElement.removeEventListener("click", this.onDelete);
    }

    public onSave() {
        const episode = {
            id: this.episode != null ? this.episode.id : null,
            name: this._nameInputElement.value
        } as Episode;
        
        this.dispatchEvent(new EpisodeAdd(episode));            
    }

    public onDelete() {        
        const episode = {
            id: this.episode != null ? this.episode.id : null,
            name: this._nameInputElement.value
        } as Episode;

        this.dispatchEvent(new EpisodeDelete(episode));         
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "episode-id":
                this.episodeId = newValue;
                break;
            case "episode":
                this.episode = JSON.parse(newValue);
                if (this.parentNode) {
                    this.episodeId = this.episode.id;
                    this._nameInputElement.value = this.episode.name != undefined ? this.episode.name : "";
                    this._titleElement.textContent = this.episodeId ? "Edit Episode" : "Create Episode";
                }
                break;
        }           
    }

    public episodeId: any;
    public episode: Episode;
    
    private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".episode-name") as HTMLInputElement;}
}

customElements.define(`ce-episode-edit-embed`,EpisodeEditEmbedComponent);
