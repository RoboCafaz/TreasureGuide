﻿import { bindable, computedFrom, customElement } from 'aurelia-framework';
import { autoinject } from 'aurelia-dependency-injection';
import { UnitQueryService } from '../services/query/unit-query-service';
import { DialogService } from 'aurelia-dialog';
import { UnitPicker } from './unit-picker';

@autoinject
@customElement('unit-display')
export class UnitDisplay {
    private element: Element;
    private unitQueryService: UnitQueryService;
    private dialogService: DialogService;

    @bindable unitId = 0;
    @bindable editable = false;

    constructor(unitQueryService: UnitQueryService, dialogService: DialogService, element: Element) {
        this.unitQueryService = unitQueryService;
        this.element = element;
        this.dialogService = dialogService;
    }

    @computedFrom('unitId')
    get unit() {
        return this.unitQueryService.stub(this.unitId).then(result => {
            return result;
        }).catch(error => {
            console.error(error);
            return null;
        });
    };

    @computedFrom('unitId')
    get imageUrl() {
        return this.unitQueryService.getIcon(this.unitId);
    }

    unitClicked() {
        if (this.editable) {
            this.dialogService.open({ viewModel: UnitPicker, lock: true }).whenClosed(result => {
                if (!result.wasCancelled) {
                    this.unitId = result.output;
                }
            });
        }
    }
}