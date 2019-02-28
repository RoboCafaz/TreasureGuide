﻿import { autoinject } from 'aurelia-framework';
import { HttpEngine } from '../../tools/http-engine';
import { IUnitStubModel, SearchConstants, IUnitSearchModel, UnitClass, UnitType, UnitFlag } from '../../models/imported';
import { SearchModel } from '../../models/search-model';
import { LocallySearchedQueryService } from './generic/locally-searched-query-service';
import { BoxService } from '../box-service';

@autoinject
export class UnitQueryService extends LocallySearchedQueryService<number, IUnitStubModel, UnitSearchModel> {
    private boxService: BoxService;

    constructor(http: HttpEngine, boxService: BoxService) {
        super('unit', http);
        this.boxService = boxService;
    }

    static getIcon(unitId: number) {
        if (unitId) {
            var id = ('0000' + unitId).slice(-4).replace(/(057[54])/, '0$1'); // missing aokiji image
            switch (id) {
                case '0742': return 'https://onepiece-treasurecruise.com/wp-content/uploads/f0742-2.png';
                case '2440': return '/content/units/2440.png';
                case '2441': return '/content/units/2441.png';
                case '2500': return '/content/units/2500.png';
                case '2502': return 'http://onepiece-treasurecruise.com/en/wp-content/uploads/sites/2/f5013.png';
                case '2503': return 'http://onepiece-treasurecruise.com/en/wp-content/uploads/sites/2/f5014.png';
                case '2504': return 'http://onepiece-treasurecruise.com/en/wp-content/uploads/sites/2/f5015.png';
                case '2505': return '/content/units/2505.png';
                case '2506': return '/content/units/2506.png';
                case '2507': return '/content/units/2507.png';
                case '2510': return '/content/units/2510.png';
                case '2511': return '/content/units/2511.png';
                case '2512': return '/content/units/2512.png';
                case '2513': return '/content/units/2513.png';
                case '2514': return '/content/units/2514.png';
                case '2515': return '/content/units/2515.png';
                case '2516': return '/content/units/2516.png';
                case '2517': return '/content/units/2517.png';
                case '2518': return '/content/units/2518.png';
                case '2519': return '/content/units/2519.png';
                case '2520': return '/content/units/2520.png';
                case '2521': return '/content/units/2521.png';
                case '2522': return '/content/units/2522.png';
            }
            return 'https://onepiece-treasurecruise.com/wp-content/uploads/f' + id + '.png';
        }
        return null;
    }

    protected performSearch(items: IUnitStubModel[], searchModel: UnitSearchModel): IUnitStubModel[] {
        items = this.searchTerm(items, searchModel.term);
        items = this.searchTypes(items, searchModel.types);
        items = this.searchClasses(items, searchModel.classes, searchModel.forceClass);
        items = this.searchGlobal(items, searchModel.global);
        items = this.searchBox(items, searchModel.myBox);
        items = this.searchLimit(items, searchModel.limitTo);
        items = this.searchFreeToPlay(items, searchModel.freeToPlay);
        return items;
    }

    private searchTerm(items: IUnitStubModel[], term: string): IUnitStubModel[] {
        return this.doTermSearch(items, (x) => x.aliases.concat(x.name), term);
    }

    private searchTypes(items: IUnitStubModel[], unitType: UnitType): IUnitStubModel[] {
        if (unitType) {
            items = items.filter(x => (x.type & unitType) !== 0);
        }
        return items;
    }

    private searchClasses(items: IUnitStubModel[], unitClass: UnitClass, forceClass: boolean): IUnitStubModel[] {
        if (unitClass) {
            if (forceClass) {
                items = items.filter(x => x.class === unitClass);
            } else {
                items = items.filter(x => (x.class & unitClass) !== 0);
            }
        }
        return items;
    }

    private searchGlobal(items: IUnitStubModel[], global: boolean): IUnitStubModel[] {
        if (global) {
            items = items.filter(x => (x.flags & UnitFlag.Global) !== 0);
        }
        return items;
    }

    private searchBox(items: IUnitStubModel[], myBox: boolean): IUnitStubModel[] {
        if (myBox) {
            var box = this.boxService.currentBox;
            if (box) {
                var boxIds = box.unitIds;
                items = items.filter(x => boxIds.indexOf(x.id) !== -1);
            }
        }
        return items;
    }

    private searchLimit(items: IUnitStubModel[], limitTo: number[]): IUnitStubModel[] {
        if (limitTo) {
            items = items.filter(x => limitTo.indexOf(x.id) !== -1);
        }
        return items;
    }

    private searchFreeToPlay(items: IUnitStubModel[], freeToPlay: boolean): IUnitStubModel[] {
        if (freeToPlay) {
            items = items.filter(x =>
                (x.flags & UnitFlag.RareRecruitExclusive) === 0 &&
                (x.flags & UnitFlag.RareRecruitLimited) === 0);
        }
        return items;
    }

    protected performSort(items: IUnitStubModel[], sortBy: string, sortDesc: boolean): IUnitStubModel[] {
        switch (sortBy) {
            case SearchConstants.SortId:
                return this.doSort(items, [x => x.id], [sortDesc]);
            case SearchConstants.SortName:
                return this.doSort(items, [x => x.name], [sortDesc]);
            case SearchConstants.SortType:
                return this.doSort(items, [x => x.type, x => x.stars, x => x.cost], [sortDesc, true, true]);
            case SearchConstants.SortClass:
                return this.doSort(items, [x => x.class, x => x.stars, x => x.cost], [sortDesc, true, true]);
            case SearchConstants.SortStars:
                return this.doSort(items, [x => x.stars, x => x.type, x => x.cost], [sortDesc, false, true]);
            default:
                return this.doSort(items, [x => x.type, x => x.stars, x => x.cost], [false, true, true]);
        }
    }
}

export class UnitSearchModel extends SearchModel implements IUnitSearchModel {
    term: string;
    classes: UnitClass;
    types: UnitType;
    forceClass: boolean;
    myBox: boolean;
    blacklist: boolean;
    global: boolean;
    freeToPlay: boolean;
    cacheKey: string = 'search-unit';
    limitTo: number[] = null;

    sortables: string[] = [
        SearchConstants.SortId,
        SearchConstants.SortName,
        SearchConstants.SortType,
        SearchConstants.SortClass,
        SearchConstants.SortStars
    ];

    public getDefault(): UnitSearchModel {
        var model = new UnitSearchModel();
        model.sortBy = SearchConstants.SortStars;
        model.sortDesc = true;
        return model;
    }
};