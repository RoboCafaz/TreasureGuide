import { autoinject, bindable, customElement } from 'aurelia-framework';
import { DialogService } from 'aurelia-dialog';
import { BindingEngine } from 'aurelia-binding';
import { TeamCommentQueryService, TeamCommentSearchModel } from '../../services/query/team-comment-query-service';
import { ITeamCommentStubModel, ITeamDetailModel, SearchConstants } from '../../models/imported';
import { PLATFORM } from 'aurelia-pal';
import { CommentDialog } from './comment-dialog';
import { AlertService } from '../../services/alert-service';
import { AlertDialog } from '../dialogs/alert-dialog';
import { AlertDialogViewModel } from '../dialogs/alert-dialog';

@autoinject
@customElement('team-comments')
export class TeamComments {
    private teamCommentService: TeamCommentQueryService;
    private element: HTMLElement;
    private bindingEngine: BindingEngine;
    private alertService: AlertService;
    private dialogService: DialogService;

    @bindable
    public team: ITeamDetailModel;
    public seen: boolean;
    public loading: boolean;
    public error: boolean;
    private comments: ITeamCommentStubModel[];

    public searchModel: TeamCommentSearchModel;

    constructor(teamCommentService: TeamCommentQueryService, element: Element,
        bindingEngine: BindingEngine, alertService: AlertService, dialogService: DialogService) {
        this.teamCommentService = teamCommentService;
        this.element = <HTMLElement>element;
        this.bindingEngine = bindingEngine;
        this.alertService = alertService;
        this.dialogService = dialogService;
    }

    refreshTimer = null;
    refreshEventHandler = () => this.refreshed();

    attached() {
        this.searchModel = <TeamCommentSearchModel>new TeamCommentSearchModel().getDefault();
        if (this.team) {
            this.searchModel.teamId = this.team.id;
        }
        this.refreshed();

        PLATFORM.global.addEventListener("scroll", this.refreshEventHandler);
        PLATFORM.global.addEventListener("resize", this.refreshEventHandler);
    }

    detached() {
        this.disableLoading();
    }

    refreshed() {
        clearTimeout(this.refreshTimer);

        this.refreshTimer = setTimeout(() => {
            if (this.isInViewport()) {
                this.loadComments();
            }
        }, 150);
    }

    loadComments() {
        this.seen = true;
        if (this.team.comments > 0) {
            this.loading = true;
            this.bindingEngine.propertyObserver(this.searchModel, 'payload').subscribe((n, o) => {
                this.search(n);
            });
            this.search(this.searchModel.payload);
        }
        this.disableLoading();
    }

    search(payload) {
        if (this.teamCommentService) {
            this.loading = true;
            this.teamCommentService.search(payload).then(x => {
                this.comments = x.results;
                this.searchModel.totalResults = x.totalResults;
                this.loading = false;
                this.error = false;
            }).catch((e) => {
                this.loading = false;
                this.error = true;
            });
        }
    }

    disableLoading() {
        PLATFORM.global.removeEventListener("scroll", this.refreshEventHandler);
        PLATFORM.global.removeEventListener("resize", this.refreshEventHandler);
    }

    isInViewport(): boolean {
        var rect = this.element.getBoundingClientRect();

        return (
            rect.top >= 0 &&
            rect.top <= (window.innerHeight || document.documentElement.clientHeight)
        );
    }

    edit(model) {
        var id = null;
        var teamId = null;
        if (model) {
            id = model.id;
            teamId = model.teamId;
        }
        this.dialogService.open({ viewModel: CommentDialog, model: model, lock: true }).whenClosed(result => {
            if (!result.wasCancelled) {
                if (!teamId) {
                    teamId = this.team.id;
                }
                this.teamCommentService.save({ id: id, teamId: teamId, text: <string>result.output }).then(result => {
                    this.alertService.success('Thank you! Your comment has been submitted.');
                    this.searchModel.sortBy = SearchConstants.SortDate;
                    this.searchModel.sortDesc = false;
                    this.search(this.searchModel.payload);
                }).catch(response => this.alertService.reportError(response));
            }
        });
    }
}