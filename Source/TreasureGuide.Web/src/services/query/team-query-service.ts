﻿import { autoinject, computedFrom } from 'aurelia-framework';
import { HttpEngine } from '../../tools/http-engine';
import { SearchableQueryService } from './generic/searchable-query-service';
import { TeamEditorModel } from '../../models/imported';

@autoinject
export class TeamQueryService extends SearchableQueryService {
    constructor(http: HttpEngine) {
        super('team', http, false);
    }

    save(model: TeamEditorModel, id?): Promise<any> {
        model.teamUnits = model.teamUnits.filter(x => x.unitId);
        return super.save(model, id);
    }
}

export class TeamSearchModel {
    term?: string;
    leaderId?: number;
    stageId?: number;
    myBox?: boolean = false;
    global?: boolean = false;
    freeToPlay?: boolean = false;
    page?: number = 1;
    pageSize?: number = 25;

    @computedFrom('term', 'leaderId', 'stageId', 'myBox', 'global', 'freeToPlay', 'page', 'pageSize')
    get payload() {
        var text = JSON.stringify({
            term: this.term,
            leaderId: this.leaderId,
            stageId: this.stageId,
            myBox: this.myBox,
            global: this.global,
            freeToPlay: this.freeToPlay,
            page: this.page,
            pageSize: this.pageSize
        });
        var output = JSON.parse(text);
        this.onChanged(output);
        return output;
    }

    onChanged: (any) => void;
};

export class TeamEditorModel {
    name = '';
    description = '';
    guide = '';
    credits = '';
    teamUnits = [];
    teamSockets = [];
    shipId = 1;
    stageId?: number;

    constructor() {
    }
};