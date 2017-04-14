import { TvShow } from "./tv-show.model";

export const tvShowActions = {
    ADD: "[TvShow] Add",
    EDIT: "[TvShow] Edit",
    DELETE: "[TvShow] Delete",
    TV_SHOWS_CHANGED: "[TvShow] TvShows Changed"
};

export class TvShowEvent extends CustomEvent {
    constructor(eventName:string, tvShow: TvShow) {
        super(eventName, {
            bubbles: true,
            cancelable: true,
            detail: { tvShow }
        });
    }
}

export class TvShowAdd extends TvShowEvent {
    constructor(tvShow: TvShow) {
        super(tvShowActions.ADD, tvShow);        
    }
}

export class TvShowEdit extends TvShowEvent {
    constructor(tvShow: TvShow) {
        super(tvShowActions.EDIT, tvShow);
    }
}

export class TvShowDelete extends TvShowEvent {
    constructor(tvShow: TvShow) {
        super(tvShowActions.DELETE, tvShow);
    }
}

export class TvShowsChanged extends CustomEvent {
    constructor(tvShows: Array<TvShow>) {
        super(tvShowActions.TV_SHOWS_CHANGED, {
            bubbles: true,
            cancelable: true,
            detail: { tvShows }
        });
    }
}
