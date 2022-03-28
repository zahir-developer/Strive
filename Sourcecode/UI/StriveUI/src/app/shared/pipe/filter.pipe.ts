import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filter'
})
export class FilterPipe implements PipeTransform {

  transform(value: any, args: any): unknown {
    if (!value || !args) {
      return value;
    }
    // filter items array, items which match and return true will be
    // kept, false will be filtered out

    return value.filter(item => (item.ModuleId).toString().indexOf((args.ModuleId).toString()) !== -1);
  }
}

