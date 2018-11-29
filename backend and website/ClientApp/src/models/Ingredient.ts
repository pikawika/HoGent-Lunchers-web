export class Ingredient{

    private _ingredientId: Number;
    private _naam : string;

    static fromJSON(json: any): Ingredient{
        if (json != null) {
            let ingredient = new Ingredient();
            ingredient.ingredientId = json.ingredientId;
            ingredient.naam = json.naam;
            return ingredient;
        }
    }

    /**
     * Getter ingredientId
     * @return {Number}
     */
	public get ingredientId(): Number {
		return this._ingredientId;
	}

    /**
     * Getter naam
     * @return {string}
     */
	public get naam(): string {
		return this._naam;
	}

    /**
     * Setter ingredientId
     * @param {Number} value
     */
	public set ingredientId(value: Number) {
		this._ingredientId = value;
	}

    /**
     * Setter naam
     * @param {string} value
     */
	public set naam(value: string) {
		this._naam = value;
	}

} 