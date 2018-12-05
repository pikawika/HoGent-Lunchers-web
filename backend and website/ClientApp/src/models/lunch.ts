import { Tag } from "./Tag";
import { Ingredient } from "./Ingredient";
import { Handelaar } from "./handelaar";

export class Lunch {
    private _lunchId: Number;
    private _naam : string;
    private _prijs : Number;
    private _ingredienten: Ingredient[]; //lijst van ingredienten (strings doordat ingredient enkel een naam heeft)
    private _beschrijving : string;
    private _afbeeldingen; //lijst van urls in vorm van string
    private _beginDatum : Date;
    private _eindDatum : Date;
    private _tags: Tag[]; //lijst van strings
    private _deleted : boolean;
    private _handelaar: Handelaar;
  
    static fromJSON(json: any): Lunch {
        
       if (json != null) {
          let lunch = new Lunch();
          lunch.lunchId = json.lunchId;
          lunch.naam = json.naam;
          lunch.prijs = json.prijs;
          lunch.ingredienten = json.lunchIngredienten;
          lunch.beschrijving = json.beschrijving;
          lunch.afbeeldingen = json.afbeeldingen;          
          lunch.tags = json.lunchTags;
          lunch.beginDatum = json.beginDatum;
          lunch.eindDatum = json.eindDatum;
          lunch.deleted = json.deleted;
          lunch.handelaar = Handelaar.fromJSON(json.handelaar);
          return lunch;
        }
    }



    /**
     * Getter ingredienten
     * @return {Ingredient[]}
     */
	public get ingredienten(): Ingredient[] {
		return this._ingredienten;
	}

    /**
     * Setter ingredienten
     * @param {Ingredient[]} value
     */
	public set ingredienten(value: Ingredient[]) {
		this._ingredienten = value;
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
     * Getter afbeeldingen
     * @return {string[]}
     */
	public get afbeeldingen(): string[] {
		return this._afbeeldingen;
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
    
	public get tags(): Tag[] {
		return this._tags;
	}

	public set tags(value: Tag[]) {
		this._tags = value;
	}
    public get handelaar(): Handelaar {
        return this._handelaar;
    }
    public set handelaar(value: Handelaar) {
        this._handelaar = value;
    }

   
}
