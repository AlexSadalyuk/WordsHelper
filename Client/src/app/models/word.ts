import { Translate } from "./translate";

export class Word {
    text: string;
    id: number;
    translate: Translate;
    learnt: boolean = false;
    /**
     *
     */
    constructor(text: string, id: number, translate: Translate) {
        this.text = text;
        this.id = id;
        this.translate = translate;
    }
}
