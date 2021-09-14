import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { APIResponse, DataResponse } from '../../models/apiModel';
import { WorkStation } from '../../models/model';
import { ApiService } from '../../services/api.service';
import { UiService } from '../../services/ui.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  displayedColumns: string[] = ['Number', 'State', 'Ip', 'Busy', 'Info', 'Action'];
  dataSource = new MatTableDataSource<WorkStation>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator | null;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private api: ApiService, private ui: UiService, public dialog: MatDialog, private router: Router) {
  }


  refreshTable() {
    this.api.getData<DataResponse<WorkStation[]>>(`WorkStation/Fetch`).subscribe(res => {
      if (!res.IsOk)
        this.ui.show(res.Message);
      else {
        res.Data.forEach(function (e) {
          e.InfoInstance = JSON.parse(e.Info);
        });

        this.dataSource = new MatTableDataSource<WorkStation>(res.Data);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      }
    });
  }

  ngOnInit(): void {
    this.refreshTable();
  }
  lock(id: string) {
    this.api.getData < APIResponse>(`workStation/lock?Id=${id}`).subscribe(res =>
    {
      if (!res.IsOk)
        this.ui.show(res.Message);
      this.refreshTable();
    });
  }
  unlock(id: string) {
    this.api.getData<APIResponse>(`workStation/unlock?Id=${id}`).subscribe(res => {
      if (!res.IsOk)
        this.ui.show(res.Message);
      this.refreshTable();
    });
  }

}
