﻿import { autoinject, bindable, customAttribute, dynamicOptions } from 'aurelia-framework';
import { AccountService } from '../services/account-service';

@dynamicOptions
@autoinject
@customAttribute('auth-req')
export class AuthReq {
    private element: Element;
    private accountService: AccountService;

    @bindable({ primaryProperty: true })
    roles: any;

    @bindable
    all: boolean;

    @bindable
    inverse: boolean;

    constructor(accountService: AccountService, element: Element) {
        this.accountService = accountService;
        this.element = element;
    }

    bind() {
        this.update();
    }

    propertyChanged(name, newValue, oldValue) {
        this.update();
    }

    private update() {
        var visibility = this.accountService.isInRoles(this.roles, this.all);
        if (this.inverse) {
            visibility = !visibility;
        }
        (<any>this.element).style.visibility = visibility ? 'visible' : 'hidden';
        (<any>this.element).style.height = visibility ? null : 0;
    }
}