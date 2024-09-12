import { AfterViewInit, Component, inject, OnInit} from '@angular/core';
import { MatOption} from '@angular/material/autocomplete'
import { MatLabel, MatFormField } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MeteoriteGrouped } from '../../models/meteorite-grouped';
import { MatTableModule } from '@angular/material/table';
import { MeteoriteService } from '../../services/meteorite.service';
import { MatSortModule, MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';

@Component({
  selector: 'app-meteorite-overview',
  standalone: true,
  imports: [
    MatSelectModule,
    MatOption,
    MatLabel, 
    MatFormField,
    MatInputModule,
    MatButtonModule,
    MatTableModule,
    MatSortModule,
    FormsModule,
    MatIconModule,
    MatFormFieldModule
  ],
  templateUrl: './meteorite-overview.component.html',
  styleUrl: './meteorite-overview.component.scss'
})
export class MeteoriteOverviewComponent implements OnInit, AfterViewInit {

  displayedColumns: string[] = ['year', 'count', 'weightTotal'];
  yearOptions: number[] = [];
  classOptions: string[] = [];  
  meteoritesGroupedData = new MatTableDataSource<MeteoriteGrouped>();
  fromOption?: number;
  toOption?: number;
  classOption?: string;
  partOfTheName?: string;

  dataSource = new MatTableDataSource<MeteoriteGrouped>();

  @ViewChild(MatSort) sort: MatSort;

  constructor(private meteoriteService: MeteoriteService) {}

  ngOnInit(): void {
    this.loadMeteoritesDictionaries();
  }

  ngAfterViewInit(): void {
    this.search();
    this.dataSource.sort = this.sort;
  }

  search() {
    console.log(this.partOfTheName);
    this.meteoriteService.getMeteoritesGroupedData({
      fromYear: this.fromOption,
      toYear: this.toOption,
      recClass: this.classOption,
      name: this.partOfTheName
    })
    .subscribe(x => {
      this.meteoritesGroupedData = new MatTableDataSource(x);
      this.meteoritesGroupedData.sort = this.sort;
    });
  }

  loadMeteoritesDictionaries() {
    this.meteoriteService.getMeteoritesDictionaries()
      .subscribe(x => {
        this.yearOptions = x.meteoritesYears;
        this.classOptions = x.meteoritesClasses;
      })
  }
}
