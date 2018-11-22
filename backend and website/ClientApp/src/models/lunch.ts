export class Lunch {
    private _lunchId: Number;
    private _naam : string;
    private _prijs : Number;
    private _ingredienten: string[]; //lijst van ingredienten (strings doordat ingredient enkel een naam heeft)
    private _beschrijving : string;
    private _afbeeldingen; //lijst van urls in vorm van string
    private _beginDatum : Date;
    private _eindDatum : Date;
    private _tags; //lijst van strings
    private _deleted : boolean;
    //private handelaar Handelaar; -> handelaar object nog aan te maken



    static fromJSON(json: any): Lunch {
        
       if (json != null) {
          let lunch = new Lunch();
          lunch.lunchId = json.lunchId;
          lunch.naam = json.naam;
          lunch.prijs = json.prijs;
          lunch.ingredienten = json.ingredienten;
          lunch.beschrijving = json.beschrijving;
          lunch.afbeeldingen = json.afbeeldingen;          
          lunch._tags = json._tags;
          lunch._beginDatum = json.beginDatum;
          lunch._eindDatum = json.eindDatum;
          lunch._deleted = json.deleted;
          return lunch;
        }
    }


    /**
     * Getter lunchId
     * @return {Number}
     */
	public get lunchId(): Number {
		return this._lunchId;
	}

    /**
     * Setter lunchId
     * @param {Number} value
     */
	public set lunchId(value: Number) {
		this._lunchId = value;
	}
    

    /**
     * Getter naam
     * @return {string}
     */
	public get naam(): string {
		return this._naam;
	}

    /**
     * Getter prijs
     * @return {Number}
     */
	public get prijs(): Number {
		return this._prijs;
	}

    /**
     * Getter beschrijving
     * @return {string}
     */
	public get beschrijving(): string {
		return this._beschrijving;
	}

    /**
     * Setter naam
     * @param {string} value
     */
	public set naam(value: string) {
		this._naam = value;
	}

    /**
     * Setter prijs
     * @param {Number} value
     */
	public set prijs(value: Number) {
		this._prijs = value;
	}

    /**
     * Setter beschrijving
     * @param {string} value
     */
	public set beschrijving(value: string) {
		this._beschrijving = value;
	}
    

    /**
     * Getter ingredienten
     * @return {string[]}
     */
	public get ingredienten(): string[] {
		return this._ingredienten;
	}

    /**
     * Getter afbeeldingen
     * @return {string[]}
     */
	public get afbeeldingen(): string[] {
		return this._afbeeldingen;
	}

    /**
     * Setter ingredienten
     * @param {string[]} value
     */
	public set ingredienten(value: string[]) {
		this._ingredienten = value;
	}

    /**
     * Setter afbeeldingen
     * @param {string[]} value
     */
	public set afbeeldingen(value: string[]) {
		this._afbeeldingen = value;
    }
    
        /**
     * Getter beginDatum
     * @return {Date}
     */
	public get beginDatum(): Date {
		return this._beginDatum;
	}

    /**
     * Getter eindDatum
     * @return {Date}
     */
	public get eindDatum(): Date {
		return this._eindDatum;
	}

    /**
     * Setter beginDatum
     * @param {Date} value
     */
	public set beginDatum(value: Date) {
		this._beginDatum = value;
	}

    /**
     * Setter eindDatum
     * @param {Date} value
     */
	public set eindDatum(value: Date) {
		this._eindDatum = value;
    }

    /**
     * Getter deleted
     * @return {boolean}
     */
	public get deleted(): boolean {
		return this._deleted;
	}

    /**
     * Setter deleted
     * @param {boolean} value
     */
	public set deleted(value: boolean) {
		this._deleted = value;
	}
    
}
