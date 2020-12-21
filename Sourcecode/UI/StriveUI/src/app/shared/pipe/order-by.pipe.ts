import { Pipe, PipeTransform } from '@angular/core';
import * as _ from 'underscore';

@Pipe({
  name: 'orderBy'
})
export class OrderByPipe implements PipeTransform {

  transform(dataArray, sortColumn, secondSortColumn, isDescending) {
    if (!dataArray) {
      return;
    } else if (sortColumn === '' || secondSortColumn === undefined) {
      return dataArray;
    } else {
      let filtered: any;
      if (isDescending) {
        if (sortColumn === 'noteDate') {
          filtered = dataArray.sort((a, b) => new Date(b.noteDate).getTime() - new Date(a.noteDate).getTime());
        } else {
          filtered = _.chain(dataArray).sortBy(function (sortRecord: any) {
            return sortRecord[sortColumn];
          }, this)
            .sortBy(function (sortRecord: any) {
              return sortRecord['orderId'];
            }, this).reverse().value();
          filtered = _.sortBy(filtered, 'orderId');
        }
      } else {
        if (sortColumn === 'noteDate') {
          filtered = dataArray.sort((a, b) => new Date(a.noteDate).getTime() - new Date(b.noteDate).getTime());
        } else {
          filtered = _.chain(dataArray).sortBy(function (sortRecord: any) {
            return sortRecord[sortColumn];
          }, this)
            .sortBy(function (sortRecord: any) {
              return sortRecord['orderId'];
            }, this).value();
        }
      }
      return filtered;
    }
  }

}
