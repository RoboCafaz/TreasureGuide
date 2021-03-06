import { autoinject, bindable, computedFrom, customElement } from 'aurelia-framework';

@autoinject
@customElement('schedule-bar')
export class ScheduleBar {
    @bindable schedule: any[] = [];
    @bindable upcoming: boolean;
    @bindable global: boolean;

    @computedFrom('upcoming')
    get header() {
        return this.upcoming ? 'Upcoming' : 'Featured';
    }

    @computedFrom('upcoming')
    get unitSize() {
        return this.upcoming ? 'tiny' : 'small';
    }

    @computedFrom('upcoming')
    get scheduleClass() {
        return (this.upcoming ? 'upcoming' : 'live') + ' ' + (this.global ? 'global' : 'japan');
    }
}