﻿import { autoinject, bindable, computedFrom } from 'aurelia-framework';
import { Router } from 'aurelia-router';
import { ProfileQueryService, ProfileEditorModel } from '../../services/query/profile-query-service';
import { ValidationControllerFactory, ValidationRules, ValidationController } from 'aurelia-validation';
import { IProfileEditorModel } from '../../models/imported';
import { BeauterValidationFormRenderer } from '../../renderers/beauter-validation-form-renderer';
import { AlertService } from '../../services/alert-service';
import { AccountService } from '../../services/account-service';

@autoinject
export class ProfileEditPage {
    public static websiteMaxLength = 200;

    private profileQueryService: ProfileQueryService;
    private router: Router;
    private alert: AlertService;
    private accountService: AccountService;

    public controller: ValidationController;

    title = 'Edit Profile';
    @bindable profile: IProfileEditorModel;

    constructor(profileQueryService: ProfileQueryService,
        router: Router,
        alertService: AlertService,
        validFactory: ValidationControllerFactory,
        accountService: AccountService
    ) {
        this.controller = validFactory.createForCurrentScope();
        this.controller.addRenderer(new BeauterValidationFormRenderer());

        this.profileQueryService = profileQueryService;
        this.router = router;
        this.alert = alertService;

        this.profile = new ProfileEditorModel();
        this.controller.addObject(this.profile);
        this.accountService = accountService;
    }

    activate(params) {
        var id = params.id;
        if (id) {
            this.profileQueryService.editor(id).then(result => {
                this.title = 'Edit Profile';
                this.profile = Object.assign(this.profile, result);
                this.controller.validate();
            }).catch(error => {
                this.router.navigateToRoute('error', { error: 'The requested profile could not be found for editing. It may not exist or you may not have permission to edit it.' });
            });
        }
        this.controller.validate();
    }

    submit() {
        this.controller.validate().then(x => {
            if (x.valid) {
                var item = this.profile;
                item.friendId = item.friendId ? parseInt(item.friendId.toString().replace(/\D/g, '')) : item.friendId;
                this.profileQueryService.save(item).then(results => {
                    this.alert.success('Successfully updated profile information!');
                    this.router.navigateToRoute('account', { id: results.id });
                }).catch(response => {
                    return response.text().then(msg => {
                        this.alert.danger(msg);
                    }).catch(error => {
                        this.alert.danger('An error has occurred. Please try again in a few moments.');
                    });
                });
            } else {
                x.results.filter(y => !y.valid && y.message).forEach(y => {
                    this.alert.danger(y.message);
                });
            }
        });
    }

    @computedFrom('profile.website')
    get webLength() {
        return (this.profile.website || '').length + '/' + ProfileEditPage.websiteMaxLength;
    }
}

ValidationRules
    .ensure((x: ProfileEditorModel) => x.friendId)
    .satisfies(x => x ? x.toString().replace(/\D/g, '').length === 9 : true)
    .withMessage('Friend Id must be a valid 9-digit number!')
    .ensure((x: ProfileEditorModel) => x.website)
    .maxLength(ProfileEditPage.websiteMaxLength)
    .on(ProfileEditorModel);