import { autoinject, bindable, computedFrom, customElement } from 'aurelia-framework';
import { UnitSearchModel } from "../services/query/unit-query-service";
import { BoxService } from '../services/box-service';

@autoinject
@customElement('unit-search')
export class UnitSearch {
    private boxService: BoxService;

    @bindable
    model: UnitSearchModel;

    @bindable
    boxLocked: boolean;

    constructor(boxService: BoxService) {
        this.boxService = boxService;
    }

    bind() {
        if (this.model) {
            if (this.boxLocked) {
                this.model.lockedFields.push('boxId');
                this.model.lockedFields.push('blacklist');
            }
        }
    }

    @computedFrom('model.boxId')
    get myBox() {
        return this.model.boxId !== undefined;
    }
    set myBox(value) {
        if (value && this.boxService.currentBox) {
            this.model.boxId = this.boxService.currentBox.id;
            this.model.blacklist = this.boxService.currentBox.blacklist;
        } else {
            this.model.boxId = undefined;
            this.model.blacklist = undefined;
        }
    }

    @computedFrom('boxService.currentBox')
    get myBoxDisabled() {
        return this.boxService.currentBox ? '' : 'disabled';
    }

    @computedFrom('boxService.currentBox')
    get myBoxTitle() {
        return this.boxService.currentBox ? 'Filter results based on your current box.' : 'You must open a box first.';
    }
}