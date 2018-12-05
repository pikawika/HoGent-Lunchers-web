export class Tag{

    private _tagId: Number;
    private naam : string;
    private _kleur : string;

    static fromJSON(json: any): Tag{
        if (json != null) {
            let tag = new Tag();
            tag.tagId = json.tagId;
            tag.naam = json.naam;
            tag.kleur = json.kleur;
            return tag;
        }
    }

    /**
     * Getter tagId
     * @return {Number}
     */
	public get tagId(): Number {
		return this._tagId;
	}

    /**
     * Getter naam
     * @return {string}
     */
	public get Naam(): string {
		return this.naam;
	}

    /**
     * Getter kleur
     * @return {string}
     */
	public get kleur(): string {
		return this._kleur;
	}

    /**
     * Setter tagId
     * @param {Number} value
     */
	public set tagId(value: Number) {
		this._tagId = value;
	}

    /**
     * Setter naam
     * @param {string} value
     */
	public set Naam(value: string) {
		this.naam = value;
	}

    /**
     * Setter kleur
     * @param {string} value
     */
	public set kleur(value: string) {
		this._kleur = value;
	}
}