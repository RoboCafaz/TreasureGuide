﻿import { BindingEngine } from 'aurelia-binding';
import { autoinject } from 'aurelia-framework';
import { TeamQueryService, TeamSearchModel } from '../../services/query/team-query-service';
import { Router } from 'aurelia-router';
import {SearchModel} from '../../models/search-model';

@autoinject
export class TeamIndexPage {
    bindingEngine: BindingEngine;
    teamQueryService: TeamQueryService;
    router: Router;

    title = 'Teams';
    teams = [];

    searchModel: TeamSearchModel;
    loading;

    constructor(teamQueryService: TeamQueryService, bindingEngine: BindingEngine, router: Router) {
        this.teamQueryService = teamQueryService;
        this.bindingEngine = bindingEngine;
        this.router = router;

    }

    activate(params) {
        this.searchModel = <TeamSearchModel>new TeamSearchModel().getCached();
        if (params) {
            this.searchModel = this.searchModel.assign(params);
        }
        this.bindingEngine.propertyObserver(this.searchModel, 'payload').subscribe((n, o) => {
            this.search(n);
        });
        this.search(this.searchModel.payload);
    }

    search(payload) {
        if (this.teamQueryService) {
            this.loading = true;
            this.router.navigateToRoute('teams', payload, { replace: false, trigger: false });
            this.teamQueryService.search(payload).then(x => {
                this.teams = x.results;
                this.searchModel.totalResults = x.totalResults;
                this.loading = false;
            }).catch((e) => {
                this.loading = false;
            });
        }
    }
}