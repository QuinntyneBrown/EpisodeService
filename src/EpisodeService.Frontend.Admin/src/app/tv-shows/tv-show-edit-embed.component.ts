import { TvShow } from "./tv-show.model";
import { EditorComponent } from "../shared";
import {  TvShowDelete, TvShowEdit, TvShowAdd } from "./tv-show.actions";

const template = require("./tv-show-edit-embed.component.html");
const styles = require("./tv-show-edit-embed.component.scss");

export class TvShowEditEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
    }

    static get observedAttributes() {
        return [
            "tv-show",
            "tv-show-id"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.tvShow ? "Edit Tv Show": "Create Tv Show";

        if (this.tvShow) {                
            this._nameInputElement.value = this.tvShow.name;  
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
        const tvShow = {
            id: this.tvShow != null ? this.tvShow.id : null,
            name: this._nameInputElement.value
        } as TvShow;
        
        this.dispatchEvent(new TvShowAdd(tvShow));            
    }

    public onDelete() {        
        const tvShow = {
            id: this.tvShow != null ? this.tvShow.id : null,
            name: this._nameInputElement.value
        } as TvShow;

        this.dispatchEvent(new TvShowDelete(tvShow));         
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "tv-show-id":
                this.tvShowId = newValue;
                break;
            case "tvShow":
                this.tvShow = JSON.parse(newValue);
                if (this.parentNode) {
                    this.tvShowId = this.tvShow.id;
                    this._nameInputElement.value = this.tvShow.name != undefined ? this.tvShow.name : "";
                    this._titleElement.textContent = this.tvShowId ? "Edit TvShow" : "Create TvShow";
                }
                break;
        }           
    }

    public tvShowId: any;
    public tvShow: TvShow;
    
    private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".tv-show-name") as HTMLInputElement;}
}

customElements.define(`ce-tv-show-edit-embed`,TvShowEditEmbedComponent);
