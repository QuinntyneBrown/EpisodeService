import { Episode } from "./episode.model";

export const episodeActions = {
    ADD: "[Episode] Add",
    EDIT: "[Episode] Edit",
    DELETE: "[Episode] Delete",
    EPISODES_CHANGED: "[Episode] Episodes Changed"
};

export class EpisodeEvent extends CustomEvent {
    constructor(eventName:string, episode: Episode) {
        super(eventName, {
            bubbles: true,
            cancelable: true,
            detail: { episode }
        });
    }
}

export class EpisodeAdd extends EpisodeEvent {
    constructor(episode: Episode) {
        super(episodeActions.ADD, episode);        
    }
}

export class EpisodeEdit extends EpisodeEvent {
    constructor(episode: Episode) {
        super(episodeActions.EDIT, episode);
    }
}

export class EpisodeDelete extends EpisodeEvent {
    constructor(episode: Episode) {
        super(episodeActions.DELETE, episode);
    }
}

export class EpisodesChanged extends CustomEvent {
    constructor(episodes: Array<Episode>) {
        super(episodeActions.EPISODES_CHANGED, {
            bubbles: true,
            cancelable: true,
            detail: { episodes }
        });
    }
}
